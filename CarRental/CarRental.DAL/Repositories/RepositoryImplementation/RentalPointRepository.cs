using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class RentalPointRepository : BaseRepository<RentalPointEntity>, IRentalPointRepository
    {
        public RentalPointRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
