using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private CarRentalDbContext _db;

        public CityRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(CityEntity item)
        {
            if (item != null)
            {
                _db.Cities.Add(item);
            }
        }

        public IQueryable<CityEntity> GetAll()
        {
            return _db.Cities;
        }

        public CityEntity Get(Guid id)
        {
            return _db.Cities.Find(id);
        }

        public void Update(CityEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var city = _db.Cities.Find(id);
            if (city != null)
            {
                _db.Cities.Remove(city);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}