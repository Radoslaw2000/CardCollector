using CardCollector.Data;
using CardCollector.Models;
using CardCollector.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardCollector.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RefreshToken>> GetActiveRefreshTokensByUserIdAsync(Guid userId)
        {
            return await _dbContext.RefreshTokens
                .Where(rt => 
                            rt.UserId == userId && 
                            rt.IsRevoked == false && 
                            rt.ExpiryDate > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task RevokeRefreshTokenAsync(Guid refreshToken)
        {
            var refreshTokenResult = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (refreshTokenResult == null)
                throw new InvalidOperationException("RefreshToken not found.");

            refreshTokenResult.IsRevoked = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
