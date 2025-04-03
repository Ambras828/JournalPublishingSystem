using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Application.DTOs;
using DataAccess.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Application.Response;

namespace JournalPublishingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles="Admin")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
     

        public UserController(IUserService userService)
        {
            _userService = userService;
         
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserQueryParameters queryParameters)
        {
            try
            {

                var result = await _userService.GetAllUsersAsync(queryParameters);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new
                    {

                        Data = result.Data,
                        TotalRecords = result.TotalRecords,
                        PageNumber = result.PageNumber,
                        PageSize = result.PageSize,
                        TotalPages = result.TotalPages
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here if necessary
                return StatusCode(500, ex.Message); // Returns a 500 Internal Server Error with the exception message
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsers([FromBody] CreateUserDTO createUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(
         new ErrorResponseValidtion
         {
             Message = "Validation Errors",
             Errors = ModelState.ToDictionary(
                 e => e.Key,
                 e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
             )
         }
     );
            try
            {

                int id = await _userService.CreateUser(createUser);



                return CreatedAtAction(nameof(GetUserById), new { id = id }, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }





        }


        [HttpGet("GetUserByNameOrEmail")]
        public async Task<IActionResult> GetUserByINameOrEmail([FromQuery] string Username, string Email)
        {
            try
            {
                var user = await _userService.GetUserByNameOrEmail(Username, Email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
             
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.UpdateUser(id, updateUser);

            if (result == null)
            {
                return NotFound($"the mentioned user with {id} was not found.");

            }
            return Ok(result);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> HardDeleteUser(int id)
        {

            try
            {

                await _userService.HardDeleteUser(id);
                return Ok($"The user with id {id} has permanently deleted");
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }



        }
        [HttpPatch("{id}/SoftDeleteUser")]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            try
            {
                await _userService.SoftDeleteUser(id);
                return Ok($"The user with Id {id} has been soft deleted");
            }

            catch(KeyNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
