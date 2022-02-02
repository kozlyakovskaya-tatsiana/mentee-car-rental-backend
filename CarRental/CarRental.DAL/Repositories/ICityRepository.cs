using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICityRepository : IBaseRepository<CityEntity>
    {
        public Task<CityEntity> GetCityByNameAsync(string name);
    }
}