using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(Guid id, bool trackChanges);
        Task<IEnumerable<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<User> GetUserByEmail(string email, bool trackChanges);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
