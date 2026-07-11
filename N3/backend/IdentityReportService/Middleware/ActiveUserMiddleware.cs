using IdentityReportService.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityReportService.Middleware
{
    /// <summary>
    /// Middleware kiểm tra trạng thái IsActive của user sau khi JWT được xác thực.
    /// Nếu tài khoản đã bị khóa (IsActive = false), trả về 401 Unauthorized ngay lập tức.
    /// </summary>
    public class ActiveUserMiddleware
    {
        private readonly RequestDelegate _next;

        public ActiveUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            // Chỉ kiểm tra khi user đã được xác thực (có token hợp lệ)
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user == null || !user.IsActive)
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ Admin.",
                            Code = "ACCOUNT_LOCKED"
                        });
                        return; // Chặn request, không đi tiếp
                    }
                }
            }

            await _next(context);
        }
    }
}
