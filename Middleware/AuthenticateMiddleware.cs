using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Middleware
{
    /// <summary>
    /// 로그인 상태 검사 미들웨어
    /// 
    /// 세선 쿠키를 확인해서 로그인한 유저만 요청을 허용한다.
    /// 필요하면 로그인 페이지로 리다이렉션을 진행한다.
    /// </summary>
    public class AuthenticateMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenticateMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // 이미 로그인 상태이면 통과
            if (await IsAuthenticatedAsync(httpContext))
            {
                await next(httpContext);
            }
            // 로그인 페이지 접속 중이면 통과
            else if (IsAuthenticating(httpContext))
            {
                await next(httpContext);
            }
            // 손님은 로그인 페이지로 안내
            else
            {
                httpContext.Response.Redirect(
                    httpContext.Request.PathBase + "/login?returnUrl="
                    + Uri.EscapeDataString(httpContext.Request.PathBase + httpContext.Request.Path));
            }
        }

        /// <summary>
        /// 경로를 검사하여 로그인 시도인지 확인한다.
        /// </summary>
        private bool IsAuthenticating(HttpContext httpContext)
            => httpContext.Request.Path.StartsWithSegments("/Login")
            || httpContext.Request.Path.StartsWithSegments("/Register");

        /// <summary>
        /// 세션을 검사하여 로그인 상태인지 확인한다.
        /// </summary>
        private async Task<bool> IsAuthenticatedAsync(HttpContext httpContext)
        {
            var isAuthenticated = false;
            // 세션 쿠키 존재유무 확인
            if (httpContext.Request.Cookies.ContainsKey("PRO_WEB_SESSION"))
            {
                // 로그인했는지 확인
                await httpContext.Session.LoadAsync();
                isAuthenticated = httpContext.Session.Keys.Contains("username");
            }
            return isAuthenticated;
        }
    }
}
