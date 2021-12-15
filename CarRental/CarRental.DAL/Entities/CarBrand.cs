using System;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class CarBrand : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
