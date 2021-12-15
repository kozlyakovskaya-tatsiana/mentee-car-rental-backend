using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class City : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //Many to one relation (Many cities for one country)
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
        public List<Location> Locations { get; set; }
        
    }
}
