using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            services.AddHttpContextAccessor();
            services.AddSwagger();
            services.AddAuth(configuration);
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
            services.AddScoped<IAuditRepository, AuditRepository>();

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

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var testing = new
            {
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                Key = configuration["Jwt:Key"]
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();
        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SSD Assignment", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

    }
}
