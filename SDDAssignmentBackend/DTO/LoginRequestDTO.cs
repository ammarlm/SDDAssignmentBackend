
using System.ComponentModel.DataAnnotations;

namespace SDDAssignmentBackend.DTO
{
    public class LoginRequestDTO
    {
        [MaxLength(100)]
        public required string UserName { get; set; }
        [MaxLength(100)]
        public required string Password { get; set; }
    }
}
