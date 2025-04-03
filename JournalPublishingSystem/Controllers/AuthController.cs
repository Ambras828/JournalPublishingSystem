using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Contracts;
using Application.DTOs;


namespace JournalPublishingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]

        public async Task<IActionResult> login([FromBody] LoginDTO loginDto)
        {
            var token = await _authService.AuthenticateUser(loginDto.Email, loginDto.Password);

            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            return Ok(new { Token = token });
        }
    }
}
