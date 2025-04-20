using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Entities;

namespace SDDAssignmentBackend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
