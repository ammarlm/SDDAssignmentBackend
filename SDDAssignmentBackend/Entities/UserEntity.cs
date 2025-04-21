using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SDDAssignmentBackend.Enums;

namespace SDDAssignmentBackend.Entities
{
    public class UserEntity : BaseEntity
    {
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        [JsonIgnore]
        [MaxLength(255)]
        public string PasswordHash { get; set; }
        
        [Required]
        [JsonIgnore]
        [MaxLength(255)]
        public string Salt { get; set; }

        [MaxLength(50)]
        public required string Role { get; set; }
    }
}
