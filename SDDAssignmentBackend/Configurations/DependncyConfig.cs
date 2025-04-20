using Microsoft.EntityFrameworkCore;
using SDDAssignmentBackend.Configurations.Options;
using SDDAssignmentBackend.Context;
using SDDAssignmentBackend.Repositories.Implementation;
using SDDAssignmentBackend.Repositories.Interface;
using SDDAssignmentBackend.Services.Implementation;
using SDDAssignmentBackend.Services.Interface;

namespace SDDAssignmentBackend.Configurations
{
    public static class DependncyConfig
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions(configuration);
            services.UseSQLDB(configuration);
            services.AddRepositories();
            services.AddServices();
        }

        public static void UseSQLDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        }
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOption>(configuration.GetSection("Jwt"));

        }
    }
}
