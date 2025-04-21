namespace SDDAssignmentBackend.Entities
{
    public class AuditEntity : BaseEntity
    {
        public required string TableName { get; set; } = "Users";
        public  required string Operation { get; set; }
        public required Guid RecordId { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public required string CreatedBy { get; set; } = "System";
    }
}
