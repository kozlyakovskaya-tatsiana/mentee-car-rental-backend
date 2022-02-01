using System.Linq;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        {
        }

        public CityEntity GetCityByName(string name)
        {
            var entity = DbSet.SingleOrDefault(city => city.Name == name);
            
            return entity;
        }
    }
}