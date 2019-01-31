using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace pro_web.Pages.Tasks
{
    public class IndexModel : PageModel
    {
        public IndexModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        public IList<Models.Task> Tasks { get; set; }

        public void OnGet()
        {
            Tasks = db.Tasks.Where(i => i.EndAt >= DateTime.Today)
                .OrderBy(i => i.EndAt)
                .AsNoTracking()
                .ToList();
        }
    }
}