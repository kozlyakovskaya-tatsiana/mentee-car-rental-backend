namespace CarRental.API.Models.Responses
{
    public class GetTokenPairResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}