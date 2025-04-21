using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SDDAssignmentBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace SDDAssignmentBackend.DTO
{
    public class UpdateUserDTO
    {
        public required Guid Id { get; set; }
        [MaxLength(100)]
        public required string Username { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleEnum Role { get; set; }
    }

    public class ChangePasswordDTO
    {
        public required Guid Id { get; set; }

        [MaxLength(100)]
        public required string OldPassword { get; set; }

        [MaxLength(100)]
        public required string NewPassword { get; set; }
    }
}
