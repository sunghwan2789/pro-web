using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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

        public Models.TaskSource Submission { get; set; }

        public string Source { get; set; }

        public async Task<IActionResult> OnGetAsync(uint submissionId)
        {
            Submission = await db.TaskSources.FindAsync(submissionId);
            if (Submission == null)
            {
                return NotFound();
            }

            // TODO: 마감 전 과제 소스코드 열람 제한

            using (var sr = new StreamReader(GetSourceStream(Submission)))
            {
                Source = await sr.ReadToEndAsync();
            }
            return Page();
        }

        private Stream GetSourceStream(Models.TaskSource submission)
        {
            var filename = $"{submission.TaskId}_{submission.AuthorId}_{submission.Sequence}.c";
            var path = Path.Combine(env.ContentRootPath, "storage", "sources", filename);
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}