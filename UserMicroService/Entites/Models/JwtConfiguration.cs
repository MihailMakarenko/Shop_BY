namespace Entities.Models
{
    public class JwtConfiguration
    {
        public string SecretKey { get; set; } = null!;
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudience { get; set; } = null!;
        public int ExpiresInMinutes { get; set; }
        public int ClockSkew { get; set; }
    }
}
