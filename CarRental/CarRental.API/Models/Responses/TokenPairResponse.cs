namespace CarRental.API.Models.Responses
{
    public class TokenPairResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}