using System;
using System.Collections.Generic;
using CarRental.Common.Enums;
using CarRental.DAL.Entities;

namespace CarRental.Business.Models.Car
{
    public class CreatingCarModel
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmissionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public CarStatus Status { get; set; }
        public List<AttachmentDTO> Photos { get; set; }
        public CarBrandModel Brand { get; set; }
        public Guid RentalPointId { get; set; }
    }
}
