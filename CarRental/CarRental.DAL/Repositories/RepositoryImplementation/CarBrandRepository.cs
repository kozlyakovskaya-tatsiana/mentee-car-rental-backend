using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private CarRentalDbContext _db;

        public CarBrandRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(CarBrandEntity item)
        {
            if (item != null)
            {
                _db.CarBrands.Add(item);
            }
        }

        public IQueryable<CarBrandEntity> GetAll()
        {
            return _db.CarBrands;
        }

        public CarBrandEntity Get(Guid id)
        {
            return _db.CarBrands.Find(id);
        }

        public void Update(CarBrandEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var carBrand = _db.CarBrands.Find(id);
            if (carBrand != null)
            {
                _db.CarBrands.Remove(carBrand);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
