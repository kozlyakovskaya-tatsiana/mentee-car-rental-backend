using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Controllers
{
    [Route("api/rentalpoint")]
    [ApiController]
    public class RentalPointController : Controller
    {
        private readonly IRentalPointService _rentalPointService;

        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public RentalPointController(
            IRentalPointService rentalPointService,
            IMapper mapper,
            ILogger<AuthController> logger
        )
        {
            _rentalPointService = rentalPointService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRentalPoint(AddNewRentalPointRequest request)
        {
            var model = _mapper.Map<AddNewRentalPointRequest, RentalPointModel>(request);
            var result = await _rentalPointService.AddNewRentalPoint(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentalPoints()
        {
            var result = await _rentalPointService.GetAllRentalPoints();

            return Ok(result);
        }
    }
}