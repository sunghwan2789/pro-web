using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Services;

namespace pro_web.Pages.Submissions
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db, IHostingEnvironment env, ICompileAndGoQueue queue)
        {
            this.db = db;
            this.env = env;
            this.queue = queue;
        }

        private readonly ProContext db;
        private readonly IHostingEnvironment env;
        private readonly ICompileAndGoQueue queue;

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

        public async Task<IActionResult> OnPostAsync([FromForm] string source)
        {
            Task = await db.Tasks.FindAsync(TaskId);
            if (Task == null)
            {
                return NotFound();
            }

            var authorId = (uint)HttpContext.Session.GetInt32("username");

            var path = Path.Combine(env.ContentRootPath, "storage", "sources", $"{Task.Id}_{authorId}_");
            var i = 1;
            while (System.IO.File.Exists(path + i + ".c"))
            {
                i++;
            }
            path += i + ".c";

            var submission = new Models.TaskSource
            {
                Task = Task,
                AuthorId = authorId,
                Sequence = (uint)i,
                Working = true,
                Error = "",
            };

            using (var fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            using (var sw = new StreamWriter(fs, new UTF8Encoding(false)))
            {
                await sw.WriteAsync(source);
            }
            submission.Size = (uint)new FileInfo(path).Length;

            db.TaskSources.Add(submission);
            await db.SaveChangesAsync();

            queue.QueueBackgroundWorkItem(submission);
            return RedirectToPage("/Submissions/Index", new
            {
                taskId = TaskId,
            });
        }
    }
}