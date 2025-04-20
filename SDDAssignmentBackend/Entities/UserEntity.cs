using System.ComponentModel.DataAnnotations;
using SDDAssignmentBackend.Enums;

namespace SDDAssignmentBackend.Entities
{
    public class UserEntity : BaseEntity
    {
        [MaxLength(100)]
        public required string Username { get; set; }
        [MaxLength(255)]
        public required string PasswordHash { get; set; }
        [MaxLength(255)]
        public required string Salt { get; set; }
        [MaxLength(50)]
        public required string Role { get; set; }
    }
}
