using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class CarBrandRepository : BaseRepository<CarBrandEntity>, ICarBrandRepository
    {
        public CarBrandRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
