using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class CarBrand : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}
