using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : BaseRepository<CarEntity>, ICarRepository
    {
        public CarRepository(CarRentalDbContext context) : base(context)
        { }
    }
}