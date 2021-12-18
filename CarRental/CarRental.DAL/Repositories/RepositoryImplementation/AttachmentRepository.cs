using CarRental.DAL.EFCore;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Repositories
{
    public class AttachmentRepository : BaseRepository<AttachmentEntity>, IAttachmentRepository
    {
        public AttachmentRepository(CarRentalDbContext context) : base(context)
        { }
    }
}
