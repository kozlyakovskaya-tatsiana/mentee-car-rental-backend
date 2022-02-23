using System.Threading.Tasks;
using AutoMapper;
using CarRental.API.Models.Requests;
using CarRental.Business.Models.Car;
using CarRental.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;

        public BookingController(
            ILogger<BookingController> logger, 
            IMapper mapper, 
            IBookingService bookingService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _bookingService = bookingService;
        }

        [Authorize]
        [HttpPatch("lock")]
        public async Task<IActionResult> LockCar([FromBody]LockCarRequest car)
        {
            var result = await _bookingService.LockCar(car.CarId);
            _logger.LogInformation("Car with id {0} locked for booking", car.CarId);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("unlock")]
        public async Task<IActionResult> UnlockCar([FromBody] UnlockCarRequest car)
        {
            var result = await _bookingService.UnlockCar(car.CarId);
            _logger.LogInformation("Car with id {0} unlocked for booking", car.CarId);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> BookCar([FromBody] BookCarRequest request)
        {
            var model = _mapper.Map<BookCarRequest, BookCarModel>(request);
            var result = await _bookingService.BookCar(model);
            _logger.LogInformation("Car with id {0} booked successful", result.CarId);

            return Ok(result);
        }
    }
}
