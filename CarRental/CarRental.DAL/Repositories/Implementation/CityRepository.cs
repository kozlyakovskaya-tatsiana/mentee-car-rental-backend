using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        {
        }

        public async Task<CityEntity> GetCityByNameAsync(string name)
        {
            var entity = DbSet.SingleOrDefault(city => city.Name == name);
            
            return entity;
        }
    }
}