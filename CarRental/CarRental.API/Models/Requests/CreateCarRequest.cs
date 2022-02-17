using System;
using System.Collections.Generic;
using CarRental.Business.Models;
using CarRental.Business.Models.Attachment;
using CarRental.Business.Models.Car;
using CarRental.Common.Enums;

namespace CarRental.API.Models.Requests
{
    public class CreateCarRequest
    {
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmissionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public List<AttachmentDTO> Photos { get; set; }
        public CarBrandModel Brand { get; set; }
        public Guid RentalPointId { get; set; }
    }
}
