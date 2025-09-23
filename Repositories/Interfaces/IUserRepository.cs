using CardCollector.Models;

namespace CardCollector.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
        Task<bool> UsernameExistAsync(string username);
        Task<bool> EmailExistAsync(string email);
        Task AddAsync(User user);

        Task SaveChangesAsync();
    }
}
