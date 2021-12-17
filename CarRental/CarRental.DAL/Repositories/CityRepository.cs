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

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(City item)
        {
            if (item != null)
            {
                _db.Cities.Add(item);
            }
        }

        public IQueryable<City> GetAll()
        {
            return _db.Cities;
        }

        public City Get(Guid id)
        {
            return _db.Cities.Find(id);
        }

        public void Update(City item)
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