using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICountryRepository : IBaseRepository<CountryEntity>
    {
        public Task<CountryEntity> GetCountryByNameAsync(string name);
    }
}