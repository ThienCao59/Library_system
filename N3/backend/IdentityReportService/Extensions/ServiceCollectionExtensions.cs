using IdentityReportService.Configuration;
using IdentityReportService.Data;
using IdentityReportService.Models;
using IdentityReportService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MassTransit;

namespace IdentityReportService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options if needed
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            return services;
        }

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();

            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Secret))
            {
                throw new InvalidOperationException("JWT Settings are not configured properly.");
            }

            if (jwtSettings.Secret.Length < 32)
            {
                throw new InvalidOperationException("JWT Secret must be at least 32 characters.");
            }

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
            var isDevelopment = configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Development";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = !isDevelopment;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Remove standard 5 minutes clock skew
                };
            });

            // Register IJwtTokenService
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthHandoffService, AuthHandoffService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEventLogService, EventLogService>();

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy =>
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(c =>
                            (c.Type == System.Security.Claims.ClaimTypes.Role ||
                             c.Type == "role" ||
                             c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            && c.Value == "Admin")));

                options.AddPolicy("RequireLibrarian", policy =>
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(c =>
                            (c.Type == System.Security.Claims.ClaimTypes.Role ||
                             c.Type == "role" ||
                             c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            && c.Value == "Librarian")));

                options.AddPolicy("RequireReader", policy =>
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(c =>
                            (c.Type == System.Security.Claims.ClaimTypes.Role ||
                             c.Type == "role" ||
                             c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            && c.Value == "Reader")));

                options.AddPolicy("RequireAdminOrLibrarian", policy =>
                    policy.RequireAssertion(ctx =>
                        ctx.User.HasClaim(c =>
                            (c.Type == System.Security.Claims.ClaimTypes.Role ||
                             c.Type == "role" ||
                             c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                            && (c.Value == "Admin" || c.Value == "Librarian"))));
            });

            return services;
        }

        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Register HttpClientFactory
            services.AddHttpClient();

            // Health Checks
            services.AddHealthChecks();

            // MassTransit Setup (Supports In-Memory for easy local testing without RabbitMQ)
            services.AddMassTransit(x =>
            {
                var useInMemory = configuration.GetValue<bool>("MassTransit:UseInMemory", true);

                if (useInMemory)
                {
                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                }
                else
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configuration["RabbitMQ:Host"] ?? "localhost", "/", h =>
                        {
                            h.Username(configuration["RabbitMQ:Username"] ?? "guest");
                            h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                        });
                    });
                }
            });

            return services;
        }
    }
}

