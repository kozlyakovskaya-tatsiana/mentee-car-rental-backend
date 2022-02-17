using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CityRepository : BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(CarRentalDbContext context) : base(context)
        {
        }

        public async Task<CityEntity> GetCityByNameAsync(string name)
        {
            var entity = await DbSet.SingleOrDefaultAsync(city => city.Name == name);
            
            return entity;
        }
    }
}