using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
