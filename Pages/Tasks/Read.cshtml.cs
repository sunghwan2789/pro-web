using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Tasks
{
    public class ReadModel : PageModel
    {
        public ReadModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        public Models.Task Task { get; set; }

        public bool OnGoing => DateTime.Today <= Task.EndAt;

        public async Task<IActionResult> OnGetAsync(uint taskId)
        {
            Task = await db.Tasks.FindAsync(taskId);
            if (Task == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}