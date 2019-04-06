using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Services;

namespace pro_web.Pages.Submissions
{
    public class RecheckModel : PageModel
    {
        public RecheckModel(ProContext db, ICompileAndGoQueue queue)
        {
            this.db = db;
            this.queue = queue;
        }

        private readonly ProContext db;
        private readonly ICompileAndGoQueue queue;

        public async Task<ActionResult> OnPostAsync(uint submissionId)
        {
            var submission = await db.Submissions.FindAsync(submissionId);
            if (submission == null)
            {
                return NotFound();
            }

            submission.Status = Models.Submission.StatusCode.SuccessOrInitialization;
            submission.Score = 0;
            submission.Working = true;
            await db.SaveChangesAsync();

            queue.QueueBackgroundWorkItem(submission);
            return RedirectToPage("/Submissions/Read", new
            {
                submissionId,
            });
        }
    }
}
