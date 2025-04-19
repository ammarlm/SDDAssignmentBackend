using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Context;

namespace SDDAssignmentBackend.Configurations
{
    public static class DependncyConfig
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration Configuration)
        {

            services.UseSQLDB(Configuration);
        }

        public static void UseSQLDB(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
        }
    }
}
