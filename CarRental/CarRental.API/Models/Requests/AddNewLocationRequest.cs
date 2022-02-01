using System;

namespace CarRental.API.Models.Requests
{
    public class AddNewLocationRequest
    {
        public Guid CityId { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
