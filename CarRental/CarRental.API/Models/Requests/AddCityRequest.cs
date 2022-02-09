using System;

namespace CarRental.API.Models.Requests
{
    public class AddCityRequest
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
