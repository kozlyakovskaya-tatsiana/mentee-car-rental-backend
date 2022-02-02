using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models;
using CarRental.Common.Exceptions;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<RentalPointModel> AddNewRentalPoint(RentalPointModel model)
        {
            var country = await _countryRepository.GetCountryByNameAsync(model.Location.Country);
            var city = await _cityRepository.GetCityByNameAsync(model.Location.City);

            if (country == null)
            {
                var newCountryEntity = new CountryEntity
                {
                    Name = model.Location.Country,
                    Id = Guid.NewGuid()
                };
                country = await _countryRepository.Add(newCountryEntity);
            }

            if (city == null)
            {
                var newCityEntity = new CityEntity()
                {
                    Name = model.Location.City,
                    Id = Guid.NewGuid(),
                    Country = country,
                    CountryId = country.Id,
                };
                city = await _cityRepository.Add(newCityEntity);
            }

            var rentalPoint = _mapper.Map<RentalPointModel, RentalPointEntity>(model);

            rentalPoint.Location.Id = rentalPoint.LocationId = Guid.NewGuid();
            rentalPoint.Location.City = city;
            rentalPoint.Location.CityId = city.Id;
            rentalPoint.Location.RentalPoint = rentalPoint;

            var entity = await _rentalPointRepository.Add(rentalPoint);
            var result = _mapper.Map<RentalPointEntity, RentalPointModel>(entity);

            return result;
        }

        public async Task<IEnumerable<RentalPointWithCoordsModel>> GetAllRentalPoints()
        {
            var query = await _rentalPointRepository.GetAll();
            var rentalPoints = await query.ToArrayAsync();
            var result = new List<RentalPointWithCoordsModel>();

            foreach (var rentalPoint in rentalPoints)
            {
                var model = _mapper.Map<RentalPointEntity, RentalPointWithCoordsModel>(rentalPoint);

                result.Add(model);
            }

            return result;
        }

        public async Task<RentalPointModel> RemoveRentalPoint(Guid id)
        {
            var rentalPoint = await _rentalPointRepository.Get(id);
            var entity = await _rentalPointRepository.Delete(rentalPoint);
            if (entity == null)
            {
                throw new NotFoundException("Rental Point doesn't exist");
            }

            var result = _mapper.Map<RentalPointEntity, RentalPointModel>(entity);

            return result;
        }
    }
}