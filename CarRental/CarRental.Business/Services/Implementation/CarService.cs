using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Car;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarBrandRepository _carBrandRepository;
        private readonly IRentalPointRepository _rentalPointRepository;

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
            _rentalPointRepository = rentalPointRepository;
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

        public async Task<CarInfoModel> CreateCar(CreatingCarModel model)
        {
            var existingBrand = _carBrandRepository.GetByName(model.Brand.Name);
            var car = _mapper.Map<CreatingCarModel, CarEntity>(model);

            car.Brand = existingBrand ?? car.Brand;

            var carEntity = await _carRepository.Add(car);
            var result = _mapper.Map<CarEntity, CarInfoModel>(carEntity);

            return result;
        }

        public async Task<IEnumerable<CarExtendedInfoModel>> GetCarsWithPaginationAndSorting(CarParameters carParameters)
        {
            var carEntities = await _carRepository.GetAll();
            var cars = carEntities.OrderBy(car => car.Brand.Name)
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize);

            return cars.Select(car => _mapper.Map<CarEntity, CarExtendedInfoModel>(car)).ToArray();
        }

        public async Task<IEnumerable<CarBrandModel>> GetCarBrands()
        {
            var carBrands = await _carBrandRepository.GetAll();

            return carBrands.Select(brand => _mapper.Map<CarBrandEntity, CarBrandModel>(brand)).ToArray();
        }

        public async Task<CarBrandModel> AddNewCarBrand(CarBrandModel model)
        {
            var mappedCarBrand = _mapper.Map<CarBrandModel, CarBrandEntity>(model);
            var carBrandEntity = await _carBrandRepository.Add(mappedCarBrand);

            var result = _mapper.Map<CarBrandEntity, CarBrandModel>(carBrandEntity);

            return result;
        }
    }
}