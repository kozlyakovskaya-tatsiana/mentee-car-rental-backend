using System;

namespace CarRental.DAL.Entities
{
    public class BookingReport : BaseEntity
    {
        public double Mark { get; set; }

        public Car Car { get; set; }
        public Guid CarId { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}