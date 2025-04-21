using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.Entities;
using SDDAssignmentBackend.Repositories.Interface;

namespace SDDAssignmentBackend.Repositories.Implementation
{
    public class AuditRepository : BaseRepository<AuditEntity>, IAuditRepository
    {
        public AuditRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
