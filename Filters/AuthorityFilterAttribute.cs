using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Filters
{
    /// <summary>
    /// 관리자 권한의 유저만 접근을 허용하는 필터
    /// </summary>
    public class AuthorityFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var db = context.HttpContext.RequestServices.GetService<ProContext>();
            var member = await db.Members.FindAsync((uint)context.HttpContext.Session.GetInt32("username"));
            // Authority 필드가 0, 즉, 관리자여야 한다.
            // 일반 유저라면 권한 없음 알림
            if (member.Authority != 0)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            else
            {
                await next.Invoke();
            }
        }
    }
}
