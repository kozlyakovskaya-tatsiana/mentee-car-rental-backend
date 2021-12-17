using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class CarBrandEntity : BaseEntity
    {
        public string Name { get; set; }
        public List<CarEntity> Cars { get; set; }
    }
}