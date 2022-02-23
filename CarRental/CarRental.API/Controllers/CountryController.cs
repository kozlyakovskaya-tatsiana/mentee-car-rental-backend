using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Identity.Policy;
using CarRental.Business.Identity.Role;
using CarRental.Business.Models.Location;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public CountryController(
            IMapper mapper, 
            ICountryService countryService
            )
        {
            _mapper = mapper;
            _countryService = countryService;
        }

        [Authorize(Policy = Policy.AdminPolicy)]
        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateCountryRequest request)
        {
            var model = _mapper.Map<CreateCountryRequest, CountryModel>(request);
            var result = await _countryService.CreateNewCountry(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCountryList()
        {
            var result = await _countryService.GetAllCountries();

            return Ok(result);
        }
    }
}
