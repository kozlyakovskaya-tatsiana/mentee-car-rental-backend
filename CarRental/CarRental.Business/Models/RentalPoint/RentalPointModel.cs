using System;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Models.RentalPoint
{
    public class RentalPointModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LocationModel Location { get; set; }
    }
}