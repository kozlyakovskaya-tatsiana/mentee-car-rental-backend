using System;
using System.Collections.Generic;
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

        private readonly IMapper _mapper;

        public CarService(
            ICarRepository carRepository, 
            IMapper mapper
            )
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<List<CarInfoModel>> GetAllCars()
        {
            var cars = await _carRepository.GetAll();
            var result = new List<CarInfoModel>();

            foreach (var car in cars)
            {
                result.Add(_mapper.Map<CarEntity, CarInfoModel>(car));
            }

            return result;
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
            throw new NotImplementedException();
        }
    }
}
