using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Place
    {
        public string Id { get; set; }
        Country country { get; set; }
        string city { get; set; }
    }
}
