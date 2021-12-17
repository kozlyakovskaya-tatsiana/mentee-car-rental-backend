using System;

namespace CarRental.DAL.Entities
{
    public class LocationEntity : BaseEntity
    {
        public string Address { get; set; }
        public double Latitude { get; set; }    
        public double Longitude { get; set; }   

        public RentalPointEntity RentalPoint { get; set; }

        public CityEntity City { get; set; }
        public Guid CityId { get; set; }
    }
}