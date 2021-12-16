using System;

namespace CarRental.DAL.Entities
{
    public class Location : BaseEntity
    {
        public string Address { get; set; }
        public double Latitude { get; set; }    
        public double Longitude { get; set; }   

        public RentalPoint RentalPoint { get; set; }

        public City City { get; set; }
        public Guid CityId { get; set; }
    }
}