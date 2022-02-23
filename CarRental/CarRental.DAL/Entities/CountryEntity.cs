using System.Collections.Generic;

namespace CarRental.DAL.Entities
{
    public class CountryEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<CityEntity> Cities { get; set; }
    }
}