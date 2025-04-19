using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SDDAssignmentBackend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
