using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Repositories.Interface;

namespace SDDAssignmentBackend.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<UserEntity?> GetByUsernameAsync(string username)
        {
            var query = AsQuerable();
            return query.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
