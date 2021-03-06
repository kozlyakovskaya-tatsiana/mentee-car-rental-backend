using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface IBookingReportRepository : IBaseRepository<BookingReportEntity>
    {
        public Task<BookingReportEntity> BookTransaction(
            UserEntity userEntity, 
            CarEntity carEntity, 
            BookingReportEntity bookingReportEntity
            );
    }
}