using System;

namespace CarRental.DAL.Entities
{
    public class AttachmentEntity : BaseEntity
    {
        public string FileFormat { get; set; }
        public byte[] Content { get; set; }

        public CarEntity Car { get; set; }
        public Guid CarId { get; set; }
    }
}