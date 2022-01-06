using System;
using System.Threading.Tasks;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Car;
using Microsoft.AspNetCore.Mvc;
using CarRental.Business.Services;

namespace CarRental.API.Controllers
{
    [Route("api/management/cars/")]
    [ApiController]
    public class CarManagementController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarManagementController(
            ICarService carService
            )
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _carService.GetAllCars();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCar([FromBody]CreateCarRequest request)
        {
            
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarInfo(Guid id)
        {
            var result = await _carService.GetCarInfo(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCar(Guid id)
        {
            var result = await _carService.RemoveCar(id);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarInfo(Guid id, [FromBody] CarInfoModel model)
        {
            var result = await _carService.ModifyCar(id, model);

            return Ok(result);
        }
    }
}
