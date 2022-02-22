using System;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.BookingReport;
using CarRental.Business.Models.Car;
using CarRental.Common.Enums;
using CarRental.Common.Exceptions;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Business.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly ICarRepository _carRepository;
        private readonly IBookingReportRepository _bookingReportRepository;

        private readonly IMapper _mapper;

        private readonly UserManager<UserEntity> _userManager;

        public BookingService(
            ICarRepository carRepository,
            IMapper mapper,
            UserManager<UserEntity> userManager, 
            IBookingReportRepository bookingReportRepository
            )
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _userManager = userManager;
            _bookingReportRepository = bookingReportRepository;
        }

        public async Task<CarInfoModel> LockCar(Guid carId)
        {
            var carEntity = await _carRepository.Get(carId);
            if (carEntity == null)
            {
                throw new NotFoundException("Car not found");
            }

            carEntity.Status = CarStatus.Locked;

            var updatedCarEntity = await _carRepository.Update(carEntity);

            return _mapper.Map<CarEntity, CarInfoModel>(updatedCarEntity);
        }

        public async Task<CarInfoModel> UnlockCar(Guid carId)
        {
            var carEntity = await _carRepository.Get(carId);
            if (carEntity == null)
            {
                throw new NotFoundException("Car not found");
            }

            carEntity.Status = CarStatus.Free;

            var updatedCarEntity = await _carRepository.Update(carEntity);

            return _mapper.Map<CarEntity, CarInfoModel>(updatedCarEntity);
        }

        public async Task<BookingReportInfoModel> BookCar(BookCarModel model)
        {
            var carEntity = await _carRepository.Get(model.CarId);
            if (carEntity == null)
            {
                throw new NotFoundException("Car not found");
            }

            var userEntity =
                await _userManager.Users.SingleAsync(user => user.NormalizedEmail == model.UserEmail.ToUpper());
            if (userEntity == null)
            {
                throw new NotFoundException("User not found");
            }
            
            var resultBookingTimeInHours = (model.EndTimeOfBooking - model.StartTimeOfBooking).TotalHours;
            var totalPrice = resultBookingTimeInHours * carEntity.PricePerHour;

            var report = new BookingReportEntity
            {
                Car = carEntity,
                StartTimeOfBooking = model.StartTimeOfBooking,
                EndTimeOfBooking = model.EndTimeOfBooking,
                User = userEntity,
                TotalPrice = totalPrice,
                Status = BookingStatus.Active,
            };

            var reportEntity = await _bookingReportRepository.BookTransaction(userEntity, carEntity, report);

            var result = _mapper.Map<BookingReportEntity, BookingReportInfoModel>(reportEntity);

            return result;
        }
    }
}