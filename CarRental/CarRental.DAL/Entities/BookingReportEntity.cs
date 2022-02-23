using System;
using CarRental.Common.Enums;

namespace CarRental.DAL.Entities
{
    public class BookingReportEntity : BaseEntity
    {
        public DateTimeOffset StartTimeOfBooking { get; set; }
        public DateTimeOffset EndTimeOfBooking { get; set; }
        public double TotalPrice { get; set; }
        public BookingStatus Status { get; set; }

        public virtual CarEntity Car { get; set; }
        public Guid CarId { get; set; }

        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}