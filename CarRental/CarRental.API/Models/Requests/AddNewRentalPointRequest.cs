using System;

namespace CarRental.API.Models.Requests
{
    public class AddNewRentalPointRequest
    {
        public string Name { get; set; }
        public AddNewLocationRequest Location { get; set; }
    }
}