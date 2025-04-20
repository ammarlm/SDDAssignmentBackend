namespace SDDAssignmentBackend.Configurations.Options
{
    public class JwtOption
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expired { get; set; }
    }
}
