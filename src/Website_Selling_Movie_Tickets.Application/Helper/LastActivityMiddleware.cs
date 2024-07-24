using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinTuc.Application.Helper
{
    public class LastActivityMiddleware
    {
        private readonly RequestDelegate _next;

        public LastActivityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy thời gian hoạt động cuối cùng từ cookies
            string lastActivityTime = context.Request.Cookies["LastActivityTime"];

            if (!string.IsNullOrEmpty(lastActivityTime) && DateTime.TryParse(lastActivityTime, out DateTime lastActivity))
            {
                // Kiểm tra nếu thời gian hiện tại quá 3 phút kể từ lần hoạt động cuối cùng
                if (DateTime.Now - lastActivity > TimeSpan.FromMinutes(3))
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            // Cập nhật thời gian cuối cùng
            CookieOptions options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(3)
            };
            context.Response.Cookies.Append("LastActivityTime", DateTime.Now.ToString(), options);

            await _next(context);
        }
    }
}
