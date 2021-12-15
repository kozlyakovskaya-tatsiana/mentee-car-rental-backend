using System;

namespace CarRental.DAL.Entities
{
    public class Attachment
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public string FileFormat { get; set; }
        public byte[] Content { get; set; }
    }
}