using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private CarRentalDbContext _db;

        public CarRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(CarEntity item)
        {
            if (item != null)
            {
                _db.Cars.Add(item);
            }
        }

        public IQueryable<CarEntity> GetAll()
        {
            return _db.Cars;
        }

        public CarEntity Get(Guid id)
        {
            return _db.Cars.Find(id);
        }

        public void Update(CarEntity item)
        {
            // relook
            _db.Entry(item).State = EntityState.Modified;
        }

        // Entity Result
        public void Remove(Guid id)
        {
            var car = _db.Cars.Find(id);
            if (car != null)
            {
                _db.Cars.Remove(car);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}