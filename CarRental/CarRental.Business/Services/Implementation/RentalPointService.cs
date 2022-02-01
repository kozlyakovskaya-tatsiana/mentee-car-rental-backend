using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Business.Models;
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

        private readonly IMapper _mapper;

        public RentalPointService(
            IRentalPointRepository rentalPointRepository,
            IMapper mapper,
            ILocationRepository locationRepository, 
            ICityRepository cityRepository
            )
        {
            _rentalPointRepository = rentalPointRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _cityRepository = cityRepository;
        }

        public async Task<RentalPointModel> AddNewRentalPoint(RentalPointModel model)
        {
            var rentalPoint = _mapper.Map<RentalPointModel, RentalPointEntity>(model);

            var location = rentalPoint.Location;

            location.Id = Guid.NewGuid();
            rentalPoint.Id = Guid.NewGuid();
            location.RentalPoint = rentalPoint;
            location.City = await _cityRepository.Get(rentalPoint.Location.CityId);

            rentalPoint.Location = location;
            rentalPoint.LocationId = location.Id;
            
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
    }
}