using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro_web.Pages.Submissions
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db, IWebHostEnvironment env, ICompileAndGoQueue queue)
        {
            this.db = db;
            this.env = env;
            this.queue = queue;
        }

        private readonly ProContext db;
        private readonly IWebHostEnvironment env;
        private readonly ICompileAndGoQueue queue;

        [FromRoute]
        public uint TaskId { get; set; }

        [BindProperty(SupportsGet = true)]
        public CompileAndGo.Languages Language { get; set; }

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

        private string LanguageExtension
            => typeof(CompileAndGo.Languages).GetMember(Language.ToString())[0]
                .GetCustomAttributes(true).OfType<DisplayAttribute>().First()
                    .ShortName;

        public async Task<IActionResult> OnPostAsync([FromForm] string source)
        {
            Task = await db.Tasks.FindAsync(TaskId);
            if (Task == null)
            {
                return NotFound();
            }

            var authorId = (uint)HttpContext.Session.GetInt32("username");

            var filename = $"{Task.Id}_{authorId}_{Language.ToString()}";
            var path = Path.Combine(env.ContentRootPath, "storage", "sources", filename);
            var i = 1;
            while (System.IO.File.Exists($"{path}_{i}{LanguageExtension}"))
            {
                i++;
            }
            filename += $"_{i}{LanguageExtension}";
            path += $"_{i}{LanguageExtension}";

            var submission = new Models.Submission
            {
                Task = Task,
                AuthorId = authorId,
                Sequence = (uint)i,
                Working = true,
                Error = "",
                Language = Language,
                Filename = filename,
            };

            using (var fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            using (var sw = new StreamWriter(fs, new UTF8Encoding(false)))
            {
                await sw.WriteAsync(source);
            }
            submission.Size = (uint)new FileInfo(path).Length;

            db.Submissions.Add(submission);
            await db.SaveChangesAsync();

            queue.QueueBackgroundWorkItem(submission);
            return RedirectToPage("/Submissions/Index", new
            {
                taskId = TaskId,
            });
        }
    }
}
