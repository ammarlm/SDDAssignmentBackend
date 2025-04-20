using SDDAssignmentBackend.DTO;
using SDDAssignmentBackend.Entities;

namespace SDDAssignmentBackend.Services.Interface
{
    public interface IUserService
    {
        Task<UserEntity> GetUser(Guid id);
        Task<UserEntity> GetUser(string username, string password);
        Task<UserEntity> CreateUser(CreateUserDTO createUserDTO);
    }
}
