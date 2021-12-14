using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
