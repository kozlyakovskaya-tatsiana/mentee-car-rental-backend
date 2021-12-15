using System;

namespace CarRental.DAL.Entities
{
    public class Attachment : BaseEntity
    {
        public string FileFormat { get; set; }
        public byte[] Content { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }
    }
}