using CarRental.Common.Enums;
using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
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
        public List<Attachment> Photos { get; set; }
        public List<Report> Reports { get; set; }
    }
}
