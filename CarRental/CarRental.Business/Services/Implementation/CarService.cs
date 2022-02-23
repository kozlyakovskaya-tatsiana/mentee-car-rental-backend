using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Car;
using CarRental.Business.Models.Responses;
using CarRental.Common.Enums;
using CarRental.Common.Helpers.PaginateHelper;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Business.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IBookingReportRepository _bookingReportRepository;

        private readonly IMapper _mapper;

        public CarService(
            ICarRepository carRepository,
            IMapper mapper,
            ICarBrandRepository carBrandRepository,
            IBookingReportRepository bookingReportRepository
        )
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _carBrandRepository = carBrandRepository;
            _bookingReportRepository = bookingReportRepository;
        }

        public async Task<IEnumerable<CarExtendedInfoModel>> GetAllCars()
        {
            var cars = _carRepository.GetAll();

            return cars.ToArray().Select(car => _mapper.Map<CarEntity, CarExtendedInfoModel>(car));
        }

        public async Task<CarInfoModel> GetCarInfo(Guid id)
        {
            var car = await _carRepository.Get(id);
            var result = _mapper.Map<CarEntity, CarInfoModel>(car);

            return result;
        }

        public async Task<CarInfoModel> RemoveCar(Guid id)
        {
            var car = await _carRepository.Get(id);
            var deletedEntity = await _carRepository.Delete(car);
            var result = _mapper.Map<CarEntity, CarInfoModel>(deletedEntity);

            return result;
        }

        public async Task<CarInfoModel> ModifyCar(Guid id, CarInfoModel model)
        {
            var car = await _carRepository.Get(id);
            var updatedCar = _mapper.Map<CarInfoModel, CarEntity>(model);
            updatedCar.Id = car.Id;

            var update = await _carRepository.Update(updatedCar);
            var result = _mapper.Map<CarEntity, CarInfoModel>(update);

            return result;
        }

        public async Task<CarInfoModel> CreateCar(CreateCarModel model)
        {
            var existingBrand = await _carBrandRepository.GetByName(model.Brand.Name);
            var car = _mapper.Map<CreateCarModel, CarEntity>(model);

            car.Brand = existingBrand ?? car.Brand;

            var carEntity = await _carRepository.Create(car);
            var result = _mapper.Map<CarEntity, CarInfoModel>(carEntity);

            return result;
        }

        public async Task<PaginatedCarsResult> GetFilteredCarsWithPagination(
            CarPaginateParameters carPaginateParameters,
            CarFilteringParameters carFilteringParameters
        )
        {
            var carEntities = _carRepository.GetAll();

            var filteredCars = carFilteringParameters != null
                ? await FilterCars(carEntities, carFilteringParameters)
                : carEntities;
            var paginatedCars = carPaginateParameters != null
                ? await PaginateCars(filteredCars, carPaginateParameters)
                : await filteredCars.ToListAsync();

            var mappedFilteredPaginatedCars =
                paginatedCars.ToArray().Select(car => _mapper.Map<CarEntity, CarExtendedInfoModel>(car));

            var result = new PaginatedCarsResult
            {
                Cars = mappedFilteredPaginatedCars.OrderBy(s => s.PricePerHour),
                TotalCarsCount = filteredCars.Count()
            };

            return result;
        }

        private async Task<IQueryable<CarEntity>> FilterCars(IQueryable<CarEntity> carEntities,
            CarFilteringParameters carFilteringParameters)
        {
            var unavailableBookedCars = await GetUnavailableBookings(
                carFilteringParameters.PickUpDateTime,
                carFilteringParameters.DropOffDateTime
            );

            return carEntities
                .Where(car => car.Status == CarStatus.Free ||
                              car.Status == CarStatus.Booked &&
                              unavailableBookedCars.FirstOrDefault(report => report.CarId == car.Id) == null
                )
                .Where(car => carFilteringParameters.BrandId == null ||
                              car.BrandId == carFilteringParameters.BrandId)
                .Where(car =>
                    carFilteringParameters.CountryId == null ||
                    car.RentalPoint.Location.City.CountryId == carFilteringParameters.CountryId)
                .Where(car =>
                    carFilteringParameters.CityId == null ||
                    car.RentalPoint.Location.CityId == carFilteringParameters.CityId)
                .Where(car =>
                    carFilteringParameters.TransmissionType == null ||
                    car.Transmission == carFilteringParameters.TransmissionType)
                .Where(car => carFilteringParameters.FuelType == null || car.Fuel == carFilteringParameters.FuelType)
                .Where(car =>
                    carFilteringParameters.QuantityOfSeats == null ||
                    car.QuantityOfSeats == carFilteringParameters.QuantityOfSeats)
                .Where(car =>
                    carFilteringParameters.LessThenPrice == null ||
                    car.PricePerHour - carFilteringParameters.LessThenPrice <= 0)
                .Where(car =>
                    carFilteringParameters.FuelConsumption == null ||
                    car.FuelConsumption - carFilteringParameters.FuelConsumption <= 0)
                .Where(car =>
                    carFilteringParameters.RentalPointId == null ||
                    car.RentalPointId == carFilteringParameters.RentalPointId);
        }

        private async Task<PaginatedList<CarEntity>> PaginateCars(IQueryable<CarEntity> filteredCars,
            CarPaginateParameters carPaginateParameters)
        {
            var paginatedCars = await PaginatedList<CarEntity>
                .PaginateList(filteredCars, carPaginateParameters.PageNumber, carPaginateParameters.PageSize);

            return paginatedCars;
        }

        private async Task<IQueryable<BookingReportEntity>> GetUnavailableBookings(
            DateTimeOffset? pickUpDateTime,
            DateTimeOffset? dropOffDateTime
        )
        {
            var reports = _bookingReportRepository.GetAll();

            return reports.Where(report =>
                pickUpDateTime < report.EndTimeOfBooking || pickUpDateTime < report.StartTimeOfBooking &&
                dropOffDateTime > report.EndTimeOfBooking);
        }
    }
}