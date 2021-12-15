using CarRental.Common.Enums;
using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public Guid BrandId { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public TransmisionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public List<Attachment> Photos { get; set; }
        public List<Report> Reports { get; set; }
    }
}
