using CardCollector.Models;

namespace CardCollector.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<IEnumerable<RefreshToken>> GetActiveRefreshTokensByUserIdAsync(Guid userId);
        Task RevokeRefreshTokenAsync(string refreshToken);
        Task SaveChangesAsync();
    }
}
