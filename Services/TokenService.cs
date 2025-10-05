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
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Username!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token object
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:EpirationInMinutes")),
                signingCredentials: creds
            );

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
