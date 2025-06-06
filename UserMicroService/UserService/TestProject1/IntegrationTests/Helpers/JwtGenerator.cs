using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserService.Tests.IntegrationTests.Helpers
{

    public static class JwtGenerator
    {
        private static IConfigurationRoot _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public static async Task<string> GenerateJwt(string email, string id, string role = "User")
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("User ID cannot be empty", nameof(id));

            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            if (_configuration == null)
                throw new ArgumentNullException(nameof(_configuration));

            var secretKey = _configuration["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured");

            var issuer = _configuration["JwtSettings:ValidIssuer"]
                ?? throw new InvalidOperationException("JWT Issuer is not configured");

            var audience = _configuration["JwtSettings:ValidAudience"]
                ?? throw new InvalidOperationException("JWT Audience is not configured");

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, email!),
        new Claim(ClaimTypes.NameIdentifier, id),
        new Claim(ClaimTypes.Role, role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
