using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class CityEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<LocationEntity> Locations { get; set; }

        public virtual CountryEntity Country { get; set; }
        public Guid CountryId { get; set; }
    }
}