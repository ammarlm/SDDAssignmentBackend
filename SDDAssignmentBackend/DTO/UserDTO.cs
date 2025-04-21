using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SDDAssignmentBackend.Enums;

namespace SDDAssignmentBackend.DTO
{
    public class CreateUserDTO
    {
        [MaxLength(100)]
        public required string Username { get; set; }
        [MaxLength(100)]
        public required string Password { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleEnum Role { get; set; }
    }
}
