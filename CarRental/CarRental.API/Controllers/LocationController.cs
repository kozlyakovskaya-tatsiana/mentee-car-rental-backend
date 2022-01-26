using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Location;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        //TODO Unfinished controller, not implemented almost all methods
        private readonly IMapper _mapper;

        private readonly ILocationService _locationService;

        public LocationController(
            IMapper mapper, 
            ILocationService locationService
            )
        {
            _mapper = mapper;
            _locationService = locationService;
        }

        [HttpPost("country")]
        public async Task<IActionResult>AddCountry(AddCountryRequest request)
        {
            var model = _mapper.Map<AddCountryRequest, CountryModel>(request);
            var result = await _locationService.AddNewCountry(model);

            return Ok(result);
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetCountryList()
        {
            var result = await _locationService.GetAllCountries();

            return Ok(result);
        }

        [HttpPost("city")]
        public async Task<IActionResult> AddCity(AddCityRequest request)
        {
            var model = _mapper.Map<AddCityRequest, CityModel>(request); 
            var result = await _locationService.AddNewCity(model);

            return Ok(result);
        }

        [HttpGet("city")]
        public async Task<IActionResult> GetCityList()
        {
            var result = await _locationService.GetAllCities();

            return Ok(result);
        }

        [HttpGet("city/{countryId}")]
        public async Task<IActionResult> GetCities(Guid countryId)
        {
            var result = await _locationService.GetCitiesByCountryId(countryId);

            return Ok(result);
        }
    }
}
