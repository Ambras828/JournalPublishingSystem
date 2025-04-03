using Application.Contracts;
using Application.DTOs;
using Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournalPublishingSystem.Controllers
{
    [Route("country")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly ICountryService _countryService;


        public CountryController(ICountryService countryService)
        {
            _countryService=countryService;
        }


        [HttpPost("AddCountry")]

        public async Task<IActionResult> AddCountry([FromBody] CountryDto countryDto )
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(new ErrorResponseValidtion
                {
                    Message = "Validation Error",
                    Errors = ModelState.ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage).ToArray())

                });

            }

           int id= await _countryService.CreateCountry(countryDto);

            return Created(nameof(AddCountry), id);


        }




    }
}
