using System;
using System.Collections.Generic;
using CarRental.Business.Models.Attachment;
using CarRental.Common.Enums;

namespace CarRental.Business.Models.Car
{
    public class CarExtendedInfoModel
    {
        public Guid Id { get; set; }
        public CarBrandModel Brand { get; set; }
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmissionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public CarStatus Status { get; set; }
        public IEnumerable<AttachmentDTO> Photos { get; set; }
    }
}
