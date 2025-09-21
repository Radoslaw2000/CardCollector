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
        /// Registers a new user with a hashed password.
        /// </summary>
        /// <param name="request">The registration request containing username, email, and password.</param>
        /// <returns>The created user entity.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the username or email already exists in the database.
        /// </exception>
        User Register(RegisterRequestDto request);

        /// <summary>
        /// Authenticates a user by verifying the provided credentials.
        /// Generates both access and refresh tokens if successful.
        /// </summary>
        /// <param name="request">The login request containing (username or email) and password.</param>
        /// <returns>
        /// A <see cref="LoginResponseDto"/> containing user ID, access token, and refresh token.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the credentials are invalid.</exception>
        LoginResponseDto Login(LoginRequestDto request);

    }
}
