using System;
using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories
{
    public class BookingReportRepository : IBookingReportRepository
    {
        private CarRentalDbContext _db;

        public BookingReportRepository(CarRentalDbContext context)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Create(BookingReport item)
        {
            if (item != null)
            {
                _db.Reports.Add(item);
            }
        }

        public IQueryable<BookingReport> GetAll()
        {
            return _db.Reports;
        }

        public BookingReport Get(Guid id)
        {
            return _db.Reports.Find(id);
        }

        public void Update(BookingReport item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(Guid id)
        {
            var bookingReport = _db.Reports.Find(id);
            if (bookingReport != null)
            {
                _db.Reports.Remove(bookingReport);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
