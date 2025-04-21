using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Repositories.Interface;

namespace SDDAssignmentBackend.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<UserEntity?> GetByUsernameAsync(string username, Guid? id)
        {
            var query = AsQuerable();
            return query.FirstOrDefaultAsync(x => x.Username == username && (id == null || x.Id != id));
        }

        public Task<PaginationResponse<UserEntity>> GetUsersAsync(int page, int pageSize, string orderBy, string orderType, string search)
        {
            var query = AsQuerable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Username.Contains(search) || x.Role.Contains(search));
            }
            if (orderBy == "username")
            {
                query = orderType == "asc" ? query.OrderBy(x => x.Username) : query.OrderByDescending(x => x.Username);
            }
            else if (orderBy == "role")
            {
                query = orderType == "asc" ? query.OrderBy(x => x.Role) : query.OrderByDescending(x => x.Role);
            }
            else
            {
                query = orderType == "asc" ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var items = query.Skip(page * pageSize).Take(pageSize).ToList();
            return Task.FromResult(new PaginationResponse<UserEntity>
            {
                TotalItems = totalCount,
                TotalPages = totalPages,
                CurrentPage = page + 1,
                PageSize = pageSize,
                Items = items
            });
        }
    }
}
