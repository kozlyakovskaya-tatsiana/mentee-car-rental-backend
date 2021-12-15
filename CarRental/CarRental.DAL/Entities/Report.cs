﻿using System;

namespace CarRental.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Guid CarId { get; set; }
        public double Mark { get; set; }
        public string Context { get; set; }
    }
}