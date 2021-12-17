using CarRental.Common.Enums;
using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Model { get; set; }
        public FuelType Fuel { get; set; }
        public double FuelConsumption { get; set; }
        public TransmissionType Transmission { get; set; }
        public int QuantityOfSeats { get; set; }
        public double PricePerHour { get; set; }
        public Status Status { get; set; }

        public List<AttachmentEntity> Photos { get; set; }

        public List<BookingReportEntity> Reports { get; set; }

        public CarBrandEntity Brand { get; set; }
        public Guid BrandId { get; set; }

        public RentalPointEntity RentalPoint { get; set; }
        public Guid RentalPointId { get; set; }
    }
}