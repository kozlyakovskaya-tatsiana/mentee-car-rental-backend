using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class RentalPointRepository : IRentalPointRepository
    {
        private CarRentalDbContext _db;

        public RentalPointRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(RentalPointEntity item)
        {
            if (item != null)
            {
                _db.RentalPoints.Add(item);
            }
        }

        public IQueryable<RentalPointEntity> GetAll()
        {
            return _db.RentalPoints;
        }

        public RentalPointEntity Get(Guid id)
        {
            return _db.RentalPoints.Find(id);
        }

        public void Update(RentalPointEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var rentalPoint = _db.RentalPoints.Find(id);
            if (rentalPoint != null)
            {
                _db.RentalPoints.Remove(rentalPoint);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
