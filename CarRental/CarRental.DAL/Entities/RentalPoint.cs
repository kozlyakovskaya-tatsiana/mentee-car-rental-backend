using System;
using System.Collections.Generic;
using CarRental.DAL.Interfaces;

namespace CarRental.DAL.Entities
{
    public class RentalPoint : IEntity
    {
        public Guid Id { get; set; }
        //For one city many Rental Points
        public City City { get; set; }
        public Guid CityId { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public List<Car> Cars { get; set; }
    }
}
