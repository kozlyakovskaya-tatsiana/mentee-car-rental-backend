using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Repositories.RepositoryInterfaces;

namespace CarRental.DAL.Repositories.RepositoryImplementation
{
    public class AttachmentRepository : BaseRepository<AttachmentEntity>, IAttachmentRepository
    {
        public AttachmentRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
