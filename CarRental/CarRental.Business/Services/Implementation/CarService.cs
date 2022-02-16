using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Car;
using CarRental.Business.Models.Responses;
using CarRental.Common.Helpers.PaginateHelper;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;

        private readonly IMapper _mapper;

        public CarService(
            ICarRepository carRepository,
            IMapper mapper,
            ICarBrandRepository carBrandRepository,
            IRentalPointRepository rentalPointRepository
        )
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _carBrandRepository = carBrandRepository;
        }

        public async Task<IEnumerable<CarInfoModel>> GetAllCars()
        {
            var cars = await _carRepository.GetAll();

            return cars.Select(car => _mapper.Map<CarEntity, CarInfoModel>(car)).ToArray();
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

            foreach (var prop in typeof(CarInfoModel).GetProperties())
            {
                var carProp = typeof(CarEntity).GetProperty(prop.Name);
                var value = prop.GetValue(model);
                if (value != null && carProp != null)
                {
                    carProp.SetValue(car, value);
                }
            }

            var update = await _carRepository.Update(car);
            var result = _mapper.Map<CarEntity, CarInfoModel>(update);

            return result;
        }

        public async Task<CarInfoModel> CreateCar(CreateCarModel model)
        {
            var existingBrand = await _carBrandRepository.GetByName(model.Brand.Name);
            var car = _mapper.Map<CreateCarModel, CarEntity>(model);

            car.Brand = existingBrand ?? car.Brand;

            var carEntity = await _carRepository.Add(car);
            var result = _mapper.Map<CarEntity, CarInfoModel>(carEntity);

            return result;
        }

        public async Task<PaginatedCarsResult> GetFilteredCarsWithPagination(
            CarPaginateParameters carPaginateParameters,
            CarFilteringParameters carFilteringParameters
        )
        {
            var carEntities = await _carRepository.GetAll();

            var filteredCars = await FilterCars(carEntities, carFilteringParameters);
            var paginatedCars = await PaginateCars(filteredCars, carPaginateParameters);

            var mappedFilteredPaginatedCars =
                paginatedCars.ToArray().Select(car => _mapper.Map<CarEntity, CarExtendedInfoModel>(car));

            var result = new PaginatedCarsResult
            {
                Cars = mappedFilteredPaginatedCars,
                TotalCarsCount = filteredCars.Count()
            };

            return result;
        }

        private Task<IQueryable<CarEntity>> FilterCars(IQueryable<CarEntity> carEntities,
            CarFilteringParameters carFilteringParameters)
        {
            var filteredCars = carEntities
                .Where(car => carFilteringParameters.BrandId == null || car.BrandId == carFilteringParameters.BrandId)
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
                    car.PricePerHour.CompareTo(carFilteringParameters.LessThenPrice) == 0)
                .Where(car =>
                    carFilteringParameters.FuelConsumption == null ||
                    car.FuelConsumption.CompareTo(carFilteringParameters.FuelConsumption) == 0);

            return Task.FromResult(filteredCars);
        }

        private async Task<PaginatedList<CarEntity>> PaginateCars(IQueryable<CarEntity> filteredCars,
            CarPaginateParameters carPaginateParameters)
        {
            var paginatedCars = await PaginatedList<CarEntity>
                .PaginateList(filteredCars, carPaginateParameters.PageNumber, carPaginateParameters.PageSize);

            return paginatedCars;
        }
    }
}