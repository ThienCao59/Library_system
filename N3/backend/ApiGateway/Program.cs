using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình từ ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Cấu hình CORS để Frontend gọi vào được
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => "API Gateway is running!");

static bool IsPublicAuthPath(PathString path)
{
    string[] publicPaths =
    [
        "/api/identity/Auth/login",
        "/api/identity/Auth/register",
        "/api/identity/Auth/refresh",
        "/api/identity/Auth/forgot-password",
        "/api/identity/Auth/reset-password"
    ];

    foreach (var publicPath in publicPaths)
    {
        if (path.StartsWithSegments(publicPath, StringComparison.OrdinalIgnoreCase))
            return true;
    }

    return false;
}

// Middleware to check IsActive on all requests that have a Bearer token
app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    
    if (!string.IsNullOrEmpty(token) && !IsPublicAuthPath(context.Request.Path))
    {
        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            // Call IdentityService to check if the user is still active
            var response = await client.GetAsync("http://identity-service:80/api/Auth/validate");
            
            // Only block if explicitly 401 (account locked/token invalid)
            // 404 means validate endpoint not deployed yet - let Ocelot handle it
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"Message\":\"Tài khoản của bạn đã bị khóa hoặc token không hợp lệ.\"}");
                return; // Stop processing the request
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error validating token: {ex.Message}");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("{\"Message\":\"Internal server error validating token.\"}");
            return;
        }
    }
    
    await next();
});

await app.UseOcelot();

app.Run();
