using SDDAssignmentBackend.DTO;

namespace SDDAssignmentBackend.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
