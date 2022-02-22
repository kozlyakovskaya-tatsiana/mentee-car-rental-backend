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
        public CarStatus Status { get; set; }

        public virtual List<AttachmentEntity> Photos { get; set; }

        public virtual List<BookingReportEntity> Reports { get; set; }

        public virtual CarBrandEntity Brand { get; set; }
        public Guid BrandId { get; set; }

        public virtual RentalPointEntity RentalPoint { get; set; }
        public Guid RentalPointId { get; set; }
    }
}