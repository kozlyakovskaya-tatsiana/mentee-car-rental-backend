using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class BookingReportRepository : BaseRepository<BookingReportEntity>, IBookingReportRepository
    {
        public BookingReportRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
