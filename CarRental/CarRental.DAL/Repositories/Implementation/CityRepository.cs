using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}