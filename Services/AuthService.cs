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

            await _refreshTokenRepository.AddAsync(refreshToken);

            var loginResponse = new LoginResponseDto
            {
                UserId = user.UserId,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token.ToString()
            };

            return loginResponse;
        }
    }
}
