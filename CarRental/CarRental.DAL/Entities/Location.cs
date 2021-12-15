using System;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class Location : IEntity
    {
        public Guid Id { get; set; }
        // Many to one relation (Many locations in one city)
        public City City { get; set; }
        public Guid CityId { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
    }
}
