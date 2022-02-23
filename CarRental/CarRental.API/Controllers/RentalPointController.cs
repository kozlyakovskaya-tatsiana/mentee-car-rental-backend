using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Identity.Policy;
using CarRental.Business.Identity.Role;
using CarRental.Business.Models.RentalPoint;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Authorization;
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

        public RentalPointController(
            IRentalPointService rentalPointService,
            IMapper mapper,
            ILogger<AuthController> logger
        )
        {
            _rentalPointService = rentalPointService;
            _mapper = mapper;
        }

        [Authorize(Policy = Policy.AdminPolicy, Roles = Role.AdminRole)]
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

        [HttpGet("city/{cityId}")]
        public async Task<IActionResult> GetRentalPointsByCity(Guid cityId)
        {
            var result = await _rentalPointService.GetRentalPointsByCity(cityId);

            return Ok(result);
        }

        [Authorize(Policy = Policy.AdminPolicy, Roles = Role.AdminRole)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveRentalPoint(Guid id)
        {
            var result = await _rentalPointService.RemoveRentalPoint(id);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalPointById(Guid id)
        {
            var result = await _rentalPointService.RemoveRentalPoint(id);

            return Ok(result);
        }
    }
}