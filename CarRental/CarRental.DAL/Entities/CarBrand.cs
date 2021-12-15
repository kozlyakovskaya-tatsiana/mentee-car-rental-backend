using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class CarBrand : BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
    }
}