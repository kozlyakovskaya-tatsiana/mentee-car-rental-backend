using carRental.Common;
using System;

namespace carRental.DAL.Entities
{
    public class Car
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public Guid CarTypeId { get; set; }
        public Guid BrandId { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public TransmisionType Transmision { get; set; }
        public int QuatityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public Attachment Photo { get; set; }
        public Report Report { get; set; }
    }
}
