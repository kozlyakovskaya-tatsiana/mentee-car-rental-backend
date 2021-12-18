using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        { }
    }
}