using SDDAssignmentBackend.Enums;

namespace SDDAssignmentBackend.DTO
{
    public class CreateUserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
