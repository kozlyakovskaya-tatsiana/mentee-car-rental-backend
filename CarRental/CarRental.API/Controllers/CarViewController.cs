using System;
using System.Threading.Tasks;
using CarRental.Business.Models.Car;
using CarRental.Business.Services;
using CarRental.Business.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarViewController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarViewController(
            ICarService carService
        )
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(
            [FromQuery] CarPaginateParameters carPaginateParameters, 
            [FromQuery] CarFilteringParameters carFilteringParameters
            )
        {
            var result = await _carService.GetFilteredCarsWithPagination(
                carPaginateParameters, 
                carFilteringParameters
                );

            return Ok(result);
        }
    }
}