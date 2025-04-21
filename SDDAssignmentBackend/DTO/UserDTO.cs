using System.ComponentModel.DataAnnotations;
using SDDAssignmentBackend.Enums;

namespace SDDAssignmentBackend.DTO
{
    public class CreateUserDTO
    {
        [MaxLength(100)]
        public required string Username { get; set; }
        [MaxLength(100)]
        public required string Password { get; set; }
        public RoleEnum Role { get; set; }
    }
}
