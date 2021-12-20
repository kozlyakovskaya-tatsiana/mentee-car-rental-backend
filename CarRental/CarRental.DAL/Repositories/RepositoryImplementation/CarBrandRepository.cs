using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class CarBrandRepository : BaseRepository<CarBrandEntity>, ICarBrandRepository
    {
        public CarBrandRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
