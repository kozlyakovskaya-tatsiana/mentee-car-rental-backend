using System;
using System.Threading.Tasks;
using CarRental.Business.Models.BookingReport;
using CarRental.Business.Models.Car;

namespace CarRental.Business.Services
{
    public interface IBookingService
    {
        public Task<CarInfoModel> LockCar(Guid carId);
        public Task<CarInfoModel> UnlockCar(Guid carId);
        public Task<BookingReportInfoModel> BookCar(BookCarModel model);
    }
}
