using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CountryRepository : BaseRepository<CountryEntity>, ICountryRepository
    {
        public CountryRepository(CarRentalDbContext context) : base(context)
        {
        }

        public async Task<CountryEntity> GetCountryByNameAsync(string name)
        {
            var entity = await DbSet.SingleOrDefaultAsync(country => country.Name == name);

            return entity;
        }
    }
}