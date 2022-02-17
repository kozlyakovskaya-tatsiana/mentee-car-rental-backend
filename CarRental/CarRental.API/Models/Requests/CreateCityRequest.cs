using System;

namespace CarRental.API.Models.Requests
{
    public class CreateCityRequest
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
