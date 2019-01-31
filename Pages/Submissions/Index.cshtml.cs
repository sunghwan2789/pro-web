using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public uint? TaskId { get; set; }
        [FromQuery]
        public uint? UserId { get; set; }
        [FromQuery]
        public uint? After { get; set; }

        public IList<Models.TaskSource> Submissions { get; set; }

        public void OnGet()
        {
            var query = db.TaskSources.AsQueryable();
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
            Submissions = query.OrderByDescending(i => i.Id).Take(120).ToList();
            ViewData["after"] = Submissions.LastOrDefault()?.Id;
        }
    }
}