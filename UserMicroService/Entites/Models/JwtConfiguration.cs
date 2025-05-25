namespace Entities.Models
{
    public class JwtConfiguration
    {
        public string SecretKey { get; set; } = null!;
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudience { get; set; } = null!;
        public int ExpiresInMinutes { get; set; } // Изменили на int
        public int ClockSkew { get; set; } // Добавили новое свойство
    }
}
