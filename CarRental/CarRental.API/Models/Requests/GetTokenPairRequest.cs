namespace CarRental.API.Models.Requests
{
    public class GetTokenPairRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}