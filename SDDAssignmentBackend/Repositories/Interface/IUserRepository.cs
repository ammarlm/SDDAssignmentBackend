using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Entities;

namespace SDDAssignmentBackend.Repositories.Interface
{
    public interface IUserRepository: IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByUsernameAsync(string username, Guid? id=null);
        Task<PaginationResponse<UserEntity>> GetUsersAsync(int page, int pageSize, string orderBy, string orderType, string search);
    }
}
