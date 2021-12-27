namespace CarRental.Business.Models.Token
{
    public class TokenRevokeModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}