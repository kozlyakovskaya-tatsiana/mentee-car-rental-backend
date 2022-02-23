using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Identity.Policy;
using CarRental.Business.Models.Location;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;

        private readonly ICityService _cityService;

        public CityController(
            IMapper mapper,
            ICityService countryService, 
            ILogger<CityController> logger
            )
        {
            _mapper = mapper;
            _cityService = countryService;
            _logger = logger;
        }

        [Authorize(Policy = Policy.AdminPolicy)]
        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityRequest request)
        {
            _logger.LogInformation("Try to create new city with name: {city}", request.Name);
            var model = _mapper.Map<CreateCityRequest, CityModel>(request);
            var result = await _cityService.CreateNewCity(model);
            _logger.LogInformation("city {city} created successful.", request.Name);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityList()
        {
            var result = await _cityService.GetAllCities();

            return Ok(result);
        }

        [HttpGet("{countryId}")]
        public async Task<IActionResult> GetCountryCities(Guid countryId)
        {
            var result = await _cityService.GetCitiesByCountryId(countryId);

            return Ok(result);
        }
    }
}
