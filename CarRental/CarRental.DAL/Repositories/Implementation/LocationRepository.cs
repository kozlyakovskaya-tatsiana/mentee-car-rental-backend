using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class LocationRepository : BaseRepository<LocationEntity>, ILocationRepository
    {
        public LocationRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}