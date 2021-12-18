using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class CarRepository : BaseRepository<CarEntity>, ICarRepository
    {
        public CarRepository(CarRentalDbContext context) : base(context)
        { }
    }
}