using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Submissions
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromRoute]
        public uint TaskId { get; set; }

        public Models.Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Task = await db.Tasks.FindAsync(TaskId);
            if (Task == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}