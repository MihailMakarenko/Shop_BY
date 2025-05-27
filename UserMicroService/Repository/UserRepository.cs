using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }



        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public async Task<IEnumerable<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(u => ids.Contains(Guid.Parse(u.Id)), trackChanges).ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(u => u.Id.Equals(id.ToString()), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmail(string email, bool trackChanges)
        {
            return await FindByCondition(u => u.NormalizedEmail!.Equals(email.ToUpper()), trackChanges).SingleOrDefaultAsync();
        }
    }
}
