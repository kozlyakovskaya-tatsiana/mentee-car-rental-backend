using System;
using CarRental.Common.Enums;

namespace CarRental.Business.Models.Car
{
    public class CarFilteringParameters
    {
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? BrandId { get; set; }
        public TransmissionType? TransmissionType { get; set; }
        public FuelType? FuelType { get; set; }
        public int? QuantityOfSeats { get; set; }
        public DateTimeOffset? PickUpDateTime { get; set; }
        public DateTimeOffset? DropOffDateTime { get; set; }
        public double? LessThenPrice { get; set; }
        public double? FuelConsumption { get; set; }
        public Guid? RentalPointId { get; set; }
    }
}
