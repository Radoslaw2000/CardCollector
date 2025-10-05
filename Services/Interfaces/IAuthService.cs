using CardCollector.DTOs.Auth;
using CardCollector.Models;

namespace CardCollector.Services.Interfaces
{
    /// <summary>
    /// Provides user authentication and registration services.
    /// Handles password hashing, login verification, and token generation.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="request">The registration details for the new user, including necessary information such as username, password, and
        /// email.</param>
        /// <returns>A task that represents the asynchronous registration operation.</returns>
        Task RegisterAsync(RegisterRequestDto request);

        /// <summary>
        /// Authenticates a user based on the provided login request.
        /// </summary>
        /// <param name="request">The login request containing user credentials.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a <see cref="LoginResponseDto"/>
        /// with the authentication result and user details.</returns>
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

        /// <summary>
        /// Logs out a user by invalidating their refresh token.
        /// </summary>
        /// <param name="logoutDto"></param>
        /// <returns></returns>
        Task LogoutAsync(LogoutDto logoutDto);

        /// <summary>
        /// Refreshes the access token using the provided refresh token.
        /// </summary>
        /// <param name="request">The login response containing the current refresh token.</param>
        Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);

    }
}
