using System;

namespace CarRental.API.Models.Requests
{
    public class CreateRentalPointRequest
    {
        public string Name { get; set; }
        public CreateLocationRequest Location { get; set; }
    }
}