using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CardCollector.Models;
using CardCollector.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CardCollector.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            // Claims that will be included in the token payload
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()), // User ID
                new Claim(JwtRegisteredClaimNames.GivenName, user.Username!),   // Username
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)           // Email
            };

            // Secret key from configuration used to sign the token and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token object
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],       // Who issued the token
                audience: _configuration["Jwt:Audience"],   // Who the token is intended for
                claims: claims,                             // User claims
                expires: DateTime.UtcNow.AddHours(3),       // Expiration time
                signingCredentials: creds                   // Digital signature
            );

            // Convert token object to a compact string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            return new RefreshToken
            {
                UserId = userId,
                Token = Guid.NewGuid(),
                ExpiryDate = DateTime.UtcNow.AddDays(3),
                IsRevoked = false
            };
        }
    }
}
