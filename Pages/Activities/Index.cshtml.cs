using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Activities
{
    public class IndexModel : PageModel
    {
        public IndexModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromRoute]
        public uint? ActivityId { get; set; }

        public Models.Activity Activity { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (ActivityId == null)
            {
                return RedirectToPage("./Archive");
            }

            Activity = await db.Activities.FindAsync(ActivityId);
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