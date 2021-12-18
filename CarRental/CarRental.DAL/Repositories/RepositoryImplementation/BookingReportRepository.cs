using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class BookingReportRepository : BaseRepository<BookingReportEntity>, IBookingReportRepository
    {
        public BookingReportRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
