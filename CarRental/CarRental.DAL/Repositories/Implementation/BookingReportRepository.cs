using System;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Common.Enums;
using CarRental.Common.Exceptions;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class BookingReportRepository : BaseRepository<BookingReportEntity>, IBookingReportRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;

        public BookingReportRepository(
            CarRentalDbContext context,
            IUserRepository userRepository,
            ICarRepository carRepository
            ) : base(context)
        {
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public async Task<BookingReportEntity> BookTransaction(
            UserEntity userEntity, 
            CarEntity carEntity, 
            BookingReportEntity bookingReportEntity
            )
        {
            await using var transaction = await Context.Database.BeginTransactionAsync();
            try
            {
                await DbSet.AddAsync(bookingReportEntity);

                userEntity.Reports.Add(bookingReportEntity);
                carEntity.Status = CarStatus.Booked;

                await _userRepository.Update(userEntity);
                await _carRepository.Update(carEntity);

                await Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new TransactionFailedException("Transaction failed");
            }

            return bookingReportEntity;
        }

        public async Task<IQueryable<BookingReportEntity>> GetBooksByCarId(Guid carId)
        {
            return DbSet.Where(report => report.CarId == carId);
        }
    }
}