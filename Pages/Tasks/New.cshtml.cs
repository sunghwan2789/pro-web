using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Pages.Tasks
{
    [AuthorityFilter]
    public class NewModel : PageModel
    {
        public NewModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [BindProperty]
        public Models.Task Task { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var task = new Models.Task();
            if (!await TryUpdateModelAsync(
                task,
                "Task",
                i => i.StartAt,
                i => i.EndAt,
                i => i.Title,
                i => i.Content,
                i => i.ExampleInput,
                i => i.ExampleOutput))
            {
                return Page();
            }

            for (var i = 0; i < Request.Form["test_score[]"].Count; i++)
            {
                task.Tests.Add(new Models.TaskTest
                {
                    Score = uint.Parse(Request.Form["test_score[]"][i]),
                    Input = Request.Form["test_input[]"][i],
                    Output = Request.Form["test_output[]"][i],
                });
            }

            db.Tasks.Add(task);
            await db.SaveChangesAsync();

            return RedirectToPage("./Read", new
            {
                TaskId = task.Id,
            });
        }
    }
}
