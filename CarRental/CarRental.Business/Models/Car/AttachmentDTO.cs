using System;

namespace CarRental.Business.Models.Car
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileFormat { get; set; }
        public string Content { get; set; }
    }
}
