using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Place
    {
        public string Id { get; set; }
        Country Country { get; set; }
        string City { get; set; }
    }
}
