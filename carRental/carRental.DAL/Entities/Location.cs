using System;

namespace carRental.DAL.Entities
{
    public class Location
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
    }
}
