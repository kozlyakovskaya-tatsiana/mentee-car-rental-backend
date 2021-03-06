using System;

namespace CarRental.Business.Models.BookingReport
{
    public class BookingReportInfoModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset StartTimeOfBooking { get; set; }
        public DateTimeOffset EndTimeOfBooking { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public Guid CarId { get; set; }
        public Guid UserId { get; set; }
    }
}
