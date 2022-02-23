using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CarRepository : BaseRepository<CarEntity>, ICarRepository
    {
        public CarRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}