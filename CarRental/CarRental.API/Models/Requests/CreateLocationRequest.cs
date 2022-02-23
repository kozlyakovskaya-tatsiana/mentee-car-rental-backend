namespace CarRental.API.Models.Requests
{
    public class CreateLocationRequest
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
