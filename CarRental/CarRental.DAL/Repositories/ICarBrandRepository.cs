using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories
{
    public interface ICarBrandRepository : IBaseRepository<CarBrandEntity>
    {
        public Task<CarBrandEntity> GetByName(string name);
    }
}