using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;

namespace CarRental.DAL.Repositories.Implementation
{
    public class AttachmentRepository : BaseRepository<AttachmentEntity>, IAttachmentRepository
    {
        public AttachmentRepository(CarRentalDbContext context) : base(context)
        {
        }
    }
}