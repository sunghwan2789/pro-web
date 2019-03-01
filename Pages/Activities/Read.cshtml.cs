using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Pages.Activities
{
    public class ReadModel : PageModel
    {
        public ReadModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        public Models.Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync(int activityId)
        {
            Activity = await db.Activities.FindAsync(activityId);
            if (Activity == null)
            {
                return NotFound();
            }
            else
            {
                return Page();
            }
        }
    }
}
