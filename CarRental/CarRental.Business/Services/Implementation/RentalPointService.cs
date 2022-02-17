using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models.RentalPoint;
using CarRental.Common.Exceptions;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Business.Services.Implementation
{
    public class RentalPointService : IRentalPointService
    {
        private readonly IRentalPointRepository _rentalPointRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        private readonly IMapper _mapper;

        public RentalPointService(
            IRentalPointRepository rentalPointRepository,
            IMapper mapper,
            ILocationRepository locationRepository,
            ICityRepository cityRepository,
            ICountryRepository countryRepository
        )
        {
            _rentalPointRepository = rentalPointRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<RentalPointModel> CreateRentalPoint(RentalPointModel model)
        {
            model.Location.City = model.Location.City.Trim();
            model.Location.Country = model.Location.Country.Trim();
            model.Location.Address = model.Location.Address.Trim();

            var existingCountry = await _countryRepository.GetCountryByNameAsync(model.Location.Country);
            var existingCity = await _cityRepository.GetCityByNameAsync(model.Location.City);

            var rentalPoint = _mapper.Map<RentalPointModel, RentalPointEntity>(model);

            rentalPoint.Location.City = existingCity ?? rentalPoint.Location.City;
            rentalPoint.Location.City.Country = existingCountry ?? rentalPoint.Location.City.Country;

            var entity = await _rentalPointRepository.Add(rentalPoint);
            var result = _mapper.Map<RentalPointEntity, RentalPointModel>(entity);

            return result;
        }

        public async Task<IEnumerable<RentalPointWithCoordsModel>> GetAllRentalPoints()
        {
            var query = _rentalPointRepository.GetAll();
            var rentalPoints = await query.ToArrayAsync();

            return rentalPoints
                .Select(rentalPoint => _mapper.Map<RentalPointEntity, RentalPointWithCoordsModel>(rentalPoint))
                .ToArray();
        }

        public async Task<RentalPointModel> RemoveRentalPoint(Guid id)
        {
            var rentalPoint = await _rentalPointRepository.Get(id);
            if (rentalPoint == null)
            {
                throw new NotFoundException("Rental Point doesn't exist");
            }

            var entity = await _rentalPointRepository.Delete(rentalPoint);

            var city = entity.Location.City;
            if (city.Locations.IsNullOrEmpty())
            {
                city = await _cityRepository.Delete(city);
            }

            var country = city.Country;
            if (country.Cities.IsNullOrEmpty())
            {
                country = await _countryRepository.Delete(country);
            }

            var result = _mapper.Map<RentalPointEntity, RentalPointModel>(entity);

            return result;
        }
    }
}