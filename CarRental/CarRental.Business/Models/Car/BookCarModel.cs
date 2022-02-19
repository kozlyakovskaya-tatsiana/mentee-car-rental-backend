using System;

namespace CarRental.Business.Models.Car
{
    public class BookCarModel
    {
        public Guid CarId { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset StartTimeOfBooking { get; set; }
        public DateTimeOffset EndTimeOfBooking { get; set; }
    }
}
