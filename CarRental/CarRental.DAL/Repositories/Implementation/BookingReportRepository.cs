using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class BookingReportRepository : BaseRepository<BookingReportEntity>, IBookingReportRepository
    {
        public BookingReportRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}