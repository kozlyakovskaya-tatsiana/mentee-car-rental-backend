using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private CarRentalDbContext _db;

        public LocationRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(Location item)
        {
            if (item != null)
            {
                _db.Locations.Add(item);
            }
        }

        public IQueryable<Location> GetAll()
        {
            return _db.Locations;
        }

        public Location Get(Guid id)
        {
            return _db.Locations.Find(id);
        }

        public void Update(Location item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var location = _db.Locations.Find(id);
            if (location != null)
            {
                _db.Locations.Remove(location);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
