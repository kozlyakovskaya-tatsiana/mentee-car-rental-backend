using CarRental.Common.Enums;
using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        //One to one relation (One car for one location)
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        //Many to one relation (Many cars for one brand)
        public Guid BrandId { get; set; }
        public CarBrand Brand { get; set; }
        public string Model { get; set; }
        public double FuelConsumption { get; set; }
        public TransmisionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        // One to many relation (Many photos for one car)
        public List<Attachment> Photos { get; set; }
        // One to many relation (Many  for one car)
        public List<Report> Reports { get; set; }
        //Many to one relation (Every car has a linked rental point)
        public Guid RentalPointId { get; set; }
        public RentalPoint RentalPoint { get; set; }
    }
}
