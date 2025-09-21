using CardCollector.Data;
using CardCollector.DTOs.Auth;
using CardCollector.Models;
using CardCollector.Services.Interfaces;

namespace CardCollector.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;

        public AuthService(ApplicationDbContext context, ITokenService tokenservice)
        {
            _dbContext = context;
            _tokenService = tokenservice;
        }

        public User Register(RegisterRequestDto request)
        {
            if (_dbContext.Users.Any(u => u.Username == request.Username))
                throw new InvalidOperationException("Username already exists.");
            if (_dbContext.Users.Any(u => u.Email == request.Email))
                throw new InvalidOperationException("Email already exists.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public LoginResponseDto Login(LoginRequestDto request)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail);

            if (user == null)
                throw new UnauthorizedAccessException("User does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid password.");

            // Revoke old refresh tokens
            var oldTokens = _dbContext.RefreshTokens.Where(rt => rt.UserId == user.UserId && !rt.IsRevoked);
            foreach (var token in oldTokens)
            {
                token.IsRevoked = true;
            }

            // Generate tokens
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.UserId);

            // Save refresh token in DB
            _dbContext.RefreshTokens.Add(refreshToken);
            _dbContext.SaveChanges();

            return new LoginResponseDto
            {
                UserId = user.UserId,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token.ToString()
            };
        }
    }
}
