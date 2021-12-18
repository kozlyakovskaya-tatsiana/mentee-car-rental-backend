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

        public CountryRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(CountryEntity item)
        {
            if (item != null)
            {
                _db.Countries.Add(item);
            }
        }

        public IQueryable<CountryEntity> GetAll()
        {
            return _db.Countries;
        }

        public CountryEntity Get(Guid id)
        {
            return _db.Countries.Find(id);
        }

        public void Update(CountryEntity item)
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