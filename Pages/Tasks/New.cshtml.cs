using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Tasks
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [BindProperty]
        public Models.Task Task { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            var member = await db.Members.FindAsync((uint)HttpContext.Session.GetInt32("username"));
            if (member.Authority != 0)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var member = await db.Members.FindAsync((uint)HttpContext.Session.GetInt32("username"));
            if (member.Authority != 0)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

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

            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();

            return RedirectToPage("./Read", new
            {
                TaskId = task.Id,
            });
        }
    }
}