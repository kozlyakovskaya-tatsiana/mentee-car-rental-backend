using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Location;
using CarRental.Business.Services;
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

        [HttpPost]
        public async Task<IActionResult> AddCountry(AddCountryRequest request)
        {
            var model = _mapper.Map<AddCountryRequest, CountryModel>(request);
            var result = await _countryService.AddNewCountry(model);

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
