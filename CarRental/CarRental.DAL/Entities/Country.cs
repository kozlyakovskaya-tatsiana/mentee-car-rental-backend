using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class Country : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
