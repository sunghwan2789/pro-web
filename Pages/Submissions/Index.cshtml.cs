using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        public IndexModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromQuery]
        public int? TaskId { get; set; }
        [FromQuery]
        public int? UserId { get; set; }
        [FromQuery]
        public int? After { get; set; }

        public IList<Models.Submission> Submissions { get; set; }

        public void OnGet()
        {
            var query = db.Submissions.AsQueryable();
            if (TaskId != null)
            {
                query = query.Where(i => i.TaskId == TaskId.Value);
            }
            if (UserId != null)
            {
                query = query.Where(i => i.AuthorId == UserId.Value);
            }
            if (After != null)
            {
                query = query.Where(i => i.Id < After);
            }
            Submissions = query.OrderByDescending(i => i.Id).Take(25).ToList();
            ViewData["after"] = Submissions.LastOrDefault()?.Id;
        }
    }
}
