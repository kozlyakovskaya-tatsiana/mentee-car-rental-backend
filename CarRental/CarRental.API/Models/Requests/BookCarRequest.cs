using System;

namespace CarRental.API.Models.Requests
{
    public class BookCarRequest
    {
        public Guid CarId { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset PickUpDateTime { get; set; }
        public DateTimeOffset DropOffDateTime { get; set; }
    }
}
