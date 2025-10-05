using CardCollector.Data;
using CardCollector.DTOs.Auth;
using CardCollector.Models;
using CardCollector.Repositories.Interfaces;
using CardCollector.Services.Interfaces;

namespace CardCollector.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthService(ITokenService tokenservice, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository)
        {
            _tokenService = tokenservice;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterRequestDto request)
        {
            if(await _userRepository.UsernameExistAsync(request.Username))
                throw new InvalidOperationException("Username already exists.");
            if(await _userRepository.EmailExistAsync(request.Email))
                throw new InvalidOperationException("Email already exists.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _userRepository.AddAsync(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = _userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail).Result;

            if (user == null)
                throw new UnauthorizedAccessException("User does not exist.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid password.");

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.UserId);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            var loginResponse = new LoginResponseDto
            {
                UserId = user.UserId,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return loginResponse;
        }

        public async Task LogoutAsync(LogoutDto logoutDto)
        {
            await _refreshTokenRepository.RevokeRefreshTokenAsync(logoutDto.RefreshToken);
        }

        public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var refreshToken = _refreshTokenRepository.GetAsync(request.RefreshToken).Result;

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiryDate <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var newAccessToken = _tokenService.GenerateAccessToken(refreshToken.User);
            var newRefreshToken = _tokenService.GenerateRefreshToken(refreshToken.UserId);

            var refreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                UserId = refreshToken.UserId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken.Token);

            var refreshTokenResponse = new RefreshTokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return refreshTokenResponse;

        }
    }
}
