using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Submissions
{
    public class ReadModel : PageModel
    {
        public ReadModel(ProContext db, IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        private readonly ProContext db;
        private readonly IHostingEnvironment env;

        public Models.Submission Submission { get; set; }

        public string Source { get; set; }

        public async Task<IActionResult> OnGetAsync(uint submissionId)
        {
            Submission = await db.Submissions.FindAsync(submissionId);
            if (Submission == null)
            {
                return NotFound();
            }

            // 마감 전 과제 소스코드 열람 제한
            var member = await db.Members.FindAsync((uint)HttpContext.Session.GetInt32("username"));
            if (
                DateTime.Today <= Submission.Task.EndAt
                && Submission.AuthorId != member.StudentNumber
                && member.Authority != 0
            )
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            using (var sr = new StreamReader(GetSourceStream(Submission)))
            {
                Source = await sr.ReadToEndAsync();
            }
            return Page();
        }

        private Stream GetSourceStream(Models.Submission submission)
        {
            var filename = $"{submission.TaskId}_{submission.AuthorId}_{submission.Sequence}.c";
            var path = Path.Combine(env.ContentRootPath, "storage", "sources", filename);
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}