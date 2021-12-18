using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        { }
    }
}