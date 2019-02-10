using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Middleware
{
    /// <summary>
    /// 기본 경로의 끝에 /를 추가하는 미들웨어
    /// </summary>
    /// <remarks>
    /// /pro로 접근하면 /pro/로 리다이렉트한다.
    /// </remarks>
    public class BasePathMiddleware
    {
        private readonly RequestDelegate next;

        public BasePathMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.HasValue)
            {
                httpContext.Response.Redirect($"{httpContext.Request.PathBase}/{httpContext.Request.QueryString}", true);
            }
            else
            {
                await next(httpContext);
            }
        }
    }
}
