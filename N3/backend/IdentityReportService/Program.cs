using System.Threading.RateLimiting;
using Fido2NetLib;
using IdentityReportService.Extensions;
using IdentityReportService.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Console()
    .WriteTo.File("Logs/identity_log-.txt", rollingInterval: RollingInterval.Day));

// Add services to the container — require JWT by default; mark public endpoints with [AllowAnonymous]
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Configure Database, Identity, and JWT from Extensions
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddIdentityConfiguration();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IdentityReportService.Services.IOtpService, IdentityReportService.Services.OtpService>();

var fido2Config = new Fido2Configuration
{
    ServerDomain = builder.Configuration["fido2:serverDomain"] ?? "localhost",
    ServerName = builder.Configuration["fido2:serverName"] ?? "SmartLib FIDO2",
    Origins = builder.Configuration.GetSection("fido2:origins").Get<HashSet<string>>() ?? new HashSet<string>(),
    TimestampDriftTolerance = builder.Configuration.GetValue<int>("fido2:timestampDriftTolerance", 300000)
};
builder.Services.AddSingleton(fido2Config);
builder.Services.AddScoped<IFido2, Fido2>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            Message = "Too many requests. Please try again in 1 minute.",
            Code = "RATE_LIMIT_EXCEEDED"
        }, token);
    };

    options.AddPolicy("auth", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("token-refresh", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

// Configure CORS to allow frontend communication
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity & Report API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token JWT theo định dạng: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var app = builder.Build();

// Tự động chạy Migration để tạo Database và các bảng khi app khởi động
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<IdentityReportService.Data.AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Enable Serilog Request Logging for clean request logs
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity & Report API v1");
        options.RoutePrefix = "swagger";
    });
}

// Only redirect to HTTPS in production, allowing seamless HTTP on localhost in development
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

// Kiểm tra IsActive: nếu tài khoản bị khóa thì chặn ngay, dù token còn hạn
app.UseMiddleware<ActiveUserMiddleware>();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
