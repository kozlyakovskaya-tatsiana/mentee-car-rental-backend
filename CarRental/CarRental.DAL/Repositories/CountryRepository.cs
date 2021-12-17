using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private CarRentalDbContext _db;

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(Country item)
        {
            if (item != null)
            {
                _db.Countries.Add(item);
            }
        }

        public IQueryable<Country> GetAll()
        {
            return _db.Countries;
        }

        public Country Get(Guid id)
        {
            return _db.Countries.Find(id);
        }

        public void Update(Country item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var country = _db.Countries.Find(id);
            if (country != null)
            {
                _db.Countries.Remove(country);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
