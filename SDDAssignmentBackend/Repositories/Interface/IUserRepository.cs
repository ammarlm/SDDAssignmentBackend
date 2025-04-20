using SDDAssignmentBackend.Entities;

namespace SDDAssignmentBackend.Repositories.Interface
{
    public interface IUserRepository: IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByUsernameAsync(string username);
    }
}
