using CardCollector.Models;

namespace CardCollector.Services.Interfaces
{
    /// <summary>
    /// Provides methods for generating and handling authentication tokens.
    /// Responsible for issuing JWT access tokens and refresh tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JSON Web Token (JWT) that will be used as an access token.
        /// </summary>
        /// <param name="user">The user for whom the token will be generated.</param>
        /// <returns>
        /// A signed JWT access token string.
        /// </returns>
        public string GenerateAccessToken(User user);

        /// <summary>
        /// Generates a new refresh token associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A new <see cref="RefreshToken"/> instance.
        /// </returns>
        public RefreshToken GenerateRefreshToken(Guid userId);
    }
}
