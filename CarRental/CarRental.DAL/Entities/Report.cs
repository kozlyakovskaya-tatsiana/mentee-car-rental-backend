using System;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class Report : IEntity
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Guid CarId { get; set; }
        public double Mark { get; set; }
        public string Context { get; set; }
    }
}
