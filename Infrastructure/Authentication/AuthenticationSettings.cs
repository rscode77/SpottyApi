namespace SpottyApi
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; } = 7;
        public string JwtIssuer { get; set; }
    }
}
