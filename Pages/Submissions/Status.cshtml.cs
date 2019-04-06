using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Submissions
{
    public class StatusModel : PageModel
    {
        public StatusModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromRoute]
        public uint TaskId { get; set; }

        public Models.Task Task { get; set; }

        public List<Models.Member> ActiveMembers { get; set; }
        
        public async Task<ActionResult> OnGetAsync()
        {
            Task = await db.Tasks.FindAsync(TaskId);
            if (Task == null)
            {
                return NotFound();
            }

            ActiveMembers = db.Members
                .Where(i => i.Authority == 0 || i.Authority == 1)
                .OrderBy(i => i.Gen)
                .ThenBy(i => i.StudentNumber)
                .ToList();

            return Page();
        }
    }
}
