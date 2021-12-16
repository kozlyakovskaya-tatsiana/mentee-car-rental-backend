using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class RentalPointRepository : IGenericRepository<RentalPoint>, IDisposable
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

        public void Create(RentalPoint item)
        {
            if (item != null)
            {
                _db.RentalPoints.Add(item);
            }
        }

        public IQueryable<RentalPoint> GetAll()
        {
            return _db.RentalPoints;
        }

        public RentalPoint Get(Guid id)
        {
            return _db.RentalPoints.Find(id);
        }

        public void Update(RentalPoint item)
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
