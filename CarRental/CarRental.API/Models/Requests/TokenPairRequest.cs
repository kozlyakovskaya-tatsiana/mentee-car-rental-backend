namespace CarRental.API.Models.Requests
{
    public class TokenPairRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}