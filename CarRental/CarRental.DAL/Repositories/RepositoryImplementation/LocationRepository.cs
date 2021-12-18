using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class LocationRepository : BaseRepository<LocationEntity>, ILocationRepository
    {
        public LocationRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
