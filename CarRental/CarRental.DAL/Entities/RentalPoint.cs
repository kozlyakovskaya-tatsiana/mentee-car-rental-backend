using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class RentalPoint : BaseEntity
    {
        // Name
        public List<Car> Cars { get; set; }

        public Location Location { get; set; }
        public Guid LocationId { get; set; }
    }
}