using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class RentalPointRepository : BaseRepository<RentalPointEntity>, IRentalPointRepository
    {
        public RentalPointRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
