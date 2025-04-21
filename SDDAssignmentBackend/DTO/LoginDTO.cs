namespace SDDAssignmentBackend.DTO
{
    public class LoginDTO
    {
        public required string Username { get; set; }
        public required string Role { get; set; }
        public required string Token { get; set; }
        public int ExpiredInMinute { get; set; }
    }
}
