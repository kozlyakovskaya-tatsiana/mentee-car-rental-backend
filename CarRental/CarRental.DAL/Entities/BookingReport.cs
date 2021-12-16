using System;

namespace CarRental.DAL.Entities
{
    public class BookingReport : BaseEntity
    {
        public DateTime StartTimeOfBooking { get; set; }
        public DateTime EndTimeOfBooking { get; set; }
        public DateTime BookingRequestDateTime { get; set; }
        public double TotalPrice { get; set; }

        // public enum for booking // request, approved, rejectedByUser, regectedByManager

        public Car Car { get; set; }
        public Guid CarId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}