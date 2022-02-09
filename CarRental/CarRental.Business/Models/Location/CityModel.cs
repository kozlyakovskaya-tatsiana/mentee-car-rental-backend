using System;
using System.Collections.Generic;

namespace CarRental.Business.Models.Location
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<LocationModel> Locations { get; set; }
        public Guid CountryId { get; set; }
    }
}