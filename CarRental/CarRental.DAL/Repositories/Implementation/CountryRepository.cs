using System.Linq;
using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CountryRepository : BaseRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(CarRentalDbContext context) : base(context)
        {
        }

        public async Task<CountryEntity> GetCountryByNameAsync(string name)
        {
            var entity = DbSet.SingleOrDefault(country => country.Name == name);

            return entity;
        }
    }
}