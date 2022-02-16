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
    public class LocationService : ILocationService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ILocationRepository _locationRepository;


        private readonly IMapper _mapper;

        public LocationService(
            ICityRepository cityRepository,
            ILocationRepository locationRepository,
            ICountryRepository countryRepository,
            IMapper mapper
        )
        {
            _cityRepository = cityRepository;
            _locationRepository = locationRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<CountryModel> AddNewCountry(CountryModel model)
        {
            var countryEntity = _mapper.Map<CountryModel, CountryEntity>(model);
            var country = await _countryRepository.Add(countryEntity);
            var result = _mapper.Map<CountryEntity, CountryModel>(country);

            return result;
        }

        public async Task<IEnumerable<CountryModel>> GetAllCountries()
        {
            var countries = await _countryRepository.GetAll();

            return countries.Select(country => _mapper.Map<CountryEntity, CountryModel>(country)).ToArray();
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
            var cities = await _cityRepository.GetAll();

            return cities.Select(city => _mapper.Map<CityEntity, CityModel>(city)).ToArray();
        }

        public async Task<IEnumerable<CityModel>> GetCitiesByCountryId(Guid countryId)
        {
            var country = await _countryRepository.Get(countryId);

            return country.Cities.Select(city => _mapper.Map<CityEntity, CityModel>(city)).ToArray();
        }
    }
}