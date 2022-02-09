using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using System.Linq;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CarBrandRepository : BaseRepository<CarBrandEntity>, ICarBrandRepository
    {
        public CarBrandRepository(CarRentalDbContext context) : base(context)
        {
        }

        public CarBrandEntity GetByName(string name)
        {
            var entity = DbSet.SingleOrDefault(brand => brand.Name == name);

            return entity;
        }
    }
}