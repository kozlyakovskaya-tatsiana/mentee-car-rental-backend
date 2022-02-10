using System;
using System.Threading.Tasks;
using AutoMapper;
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

        private readonly IMapper _mapper;

        public CarManagementController(
            ICarService carService,
            IMapper mapper
        )
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var result = await _carService.GetAllCars();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCar([FromBody] CreateCarRequest request)
        {
            var model = _mapper.Map<CreateCarRequest, CreatingCarModel>(request);
            var result = await _carService.CreateCar(model);
            
            return Ok(result);
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

        [HttpGet("brand")]
        public async Task<IActionResult> GetAllCarBrands()
        {
            var result = await _carService.GetCarBrands();

            return Ok(result);
        }

        [HttpPost("brand")]
        public async Task<IActionResult> CreateCarBrand([FromBody] CreateCarBrandRequest request)
        {
            var model = _mapper.Map<CreateCarBrandRequest, CarBrandModel>(request);
            var result = await _carService.CreateBrand(model);

            return Ok(result);
        }
    }
}