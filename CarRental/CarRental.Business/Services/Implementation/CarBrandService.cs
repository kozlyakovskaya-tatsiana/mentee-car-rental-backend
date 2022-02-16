using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Car;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class CarBrandService : ICarBrandService
    {
        private readonly ICarBrandRepository _carBrandRepository;

        private readonly IMapper _mapper;

        public CarBrandService(
            IRentalPointRepository rentalPointRepository, 
            ICarBrandRepository carBrandRepository, 
            IMapper mapper
            )
        {
            _carBrandRepository = carBrandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarBrandModel>> GetCarBrands()
        {
            var carBrands = await _carBrandRepository.GetAll();

            return carBrands.Select(brand => _mapper.Map<CarBrandEntity, CarBrandModel>(brand)).ToArray();
        }

        public async Task<CarBrandModel> CreateBrand(CarBrandModel model)
        {
            var mappedCarBrand = _mapper.Map<CarBrandModel, CarBrandEntity>(model);
            var carBrandEntity = await _carBrandRepository.Add(mappedCarBrand);

            var result = _mapper.Map<CarBrandEntity, CarBrandModel>(carBrandEntity);

            return result;
        }
    }
}