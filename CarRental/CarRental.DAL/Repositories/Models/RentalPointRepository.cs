using System;
using System.Collections.Generic;
using System.Linq;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Models
{
    class RentalPointRepository : IRepository<RentalPoint>, IDisposable
    {
        private CarRentalDbContext _carRentalDbContext;
        public void Dispose()
        {
            _carRentalDbContext.Dispose();
        }

        public IEnumerable<RentalPoint> GetEntityList()
        {
            throw new NotImplementedException();
        }

        public void Create(RentalPoint item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RentalPoint> GetAll()
        {
            throw new NotImplementedException();
        }

        public RentalPoint Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(RentalPoint item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _carRentalDbContext.SaveChanges();
        }
    }
}
