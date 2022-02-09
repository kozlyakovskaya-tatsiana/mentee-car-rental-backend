using System;
using CarRental.Business.Models.Location;

namespace CarRental.Business.Models
{
    public class RentalPointWithCoordsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual LocationModel Location { get; set; }
    }
}