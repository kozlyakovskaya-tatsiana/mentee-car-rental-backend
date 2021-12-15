﻿using CarRental.Common.Enums;
using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class Car : BaseEntity
    {
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmisionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }

        public List<Attachment> Photos { get; set; }

        public List<Report> Reports { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid BrandId { get; set; }
        public CarBrand Brand { get; set; }

        public Guid RentalPointId { get; set; }
        public RentalPoint RentalPoint { get; set; }
    }
}
