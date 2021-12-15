﻿using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public List<Location> Locations { get; set; }

        public Country Country { get; set; }
        public Guid CountryId { get; set; }
    }
}