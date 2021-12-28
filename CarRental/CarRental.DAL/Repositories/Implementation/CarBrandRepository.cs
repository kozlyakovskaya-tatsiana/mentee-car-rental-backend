using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class CarBrandRepository : BaseRepository<CarBrandEntity>, ICarBrandRepository
    {
        public CarBrandRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}