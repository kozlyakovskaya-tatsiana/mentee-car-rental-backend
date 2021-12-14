using System;
using System.Collections.Generic;

namespace carRental.DAL.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
