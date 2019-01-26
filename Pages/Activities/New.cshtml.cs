using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Activities
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        public Models.Activity Activity { get; set; }

        public IEnumerable<Models.Member> Members { get; set; }

        public void OnGet()
        {
            // TODO: 권한 확인
            // Authority 필드가 0, 즉, 관리자여야 한다.

            // 회원을 최근 2개월 간 참여 횟수로 정렬
            Members = db.Members.OrderBy(i => i.Authority)
                .ThenByDescending(i =>
                    i.ActivityAttendees.Select(j => j.Activity).Where(j =>
                        j.StartAt.Date >= DateTime.Today.AddMonths(-2)
                    )
                    .Count()
                )
                .ThenByDescending(i => i.Gen)
                .ThenBy(i => i.StudentNumber)
                .ToList();
        }
    }
}