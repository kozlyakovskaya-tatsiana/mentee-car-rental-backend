using System;
using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class RentalPointEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<CarEntity> Cars { get; set; }

        public virtual LocationEntity Location { get; set; }
        public Guid LocationId { get; set; }
    }
}