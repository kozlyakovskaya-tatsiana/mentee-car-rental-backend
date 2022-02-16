using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models;
using CarRental.Business.Models.RentalPoint;
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
        public async Task<IActionResult> CreateRentalPoint([FromBody] CreateRentalPointRequest request)
        {
            var model = _mapper.Map<CreateRentalPointRequest, RentalPointModel>(request);
            var result = await _rentalPointService.CreateRentalPoint(model);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRentalPoints()
        {
            var result = await _rentalPointService.GetAllRentalPoints();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRentalPoint(Guid id)
        {
            var result = await _rentalPointService.RemoveRentalPoint(id);

            return Ok(result);
        }
    }
}