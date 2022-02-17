using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.Location;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;

namespace CarRental.Business.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public CityService(
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IMapper mapper
        )
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CityModel> AddNewCity(CityModel model)
        {
            var cityEntity = _mapper.Map<CityModel, CityEntity>(model);
            var city = await _cityRepository.Add(cityEntity);
            var result = _mapper.Map<CityEntity, CityModel>(city);

            return result;
        }

        public async Task<IEnumerable<CityModel>> GetAllCities()
        {
            var cities = _cityRepository.GetAll();

            return cities.Select(city => _mapper.Map<CityEntity, CityModel>(city)).ToArray();
        }

        public async Task<IEnumerable<CityModel>> GetCitiesByCountryId(Guid countryId)
        {
            var country = await _countryRepository.Get(countryId);

            return country.Cities.Select(city => _mapper.Map<CityEntity, CityModel>(city)).ToArray();
        }
    }
}