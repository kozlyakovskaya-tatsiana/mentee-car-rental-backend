using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public Car Car { get; set; }
        public Guid CarId { get; set; }
        public double Mark { get; set; }
        public string ReportContext { get; set; }
    }
}
