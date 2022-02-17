using System.Threading.Tasks;
using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CarBrandRepository : BaseRepository<CarBrandEntity>, ICarBrandRepository
    {
        public CarBrandRepository(CarRentalDbContext context) : base(context)
        {
        }

        public async Task<CarBrandEntity> GetByName(string name)
        {
            var entity = await DbSet.SingleOrDefaultAsync(brand => brand.Name == name);

            return entity;
        }
    }
}