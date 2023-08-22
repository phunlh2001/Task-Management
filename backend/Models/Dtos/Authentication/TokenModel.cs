namespace backend.Models.Dtos.Authentication
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
    }
}