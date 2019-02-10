using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Models;

namespace pro_web.Pages.Activities
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db, IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        private readonly ProContext db;
        private readonly IHostingEnvironment env;

        [BindProperty]
        public Activity Activity { get; set; }

        public IEnumerable<Member> Members { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public async Task<ActionResult> OnGetAsync()
        {
            // Authority 필드가 0, 즉, 관리자여야 한다.
            var member = await db.Members.FindAsync((uint)HttpContext.Session.GetInt32("username"));
            if (member.Authority != 0)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            // 회원을 최근 2개월 간 참여 횟수로 정렬
            Members = db.Members.OrderBy(i => i.Authority)
                .ThenByDescending(i =>
                    i.ActivityAttendees.Select(j => j.Activity).Where(j =>
                        j.StartAt.Date >= DateTime.Today.AddMonths(-2)
                    )
                    .Count()
                )
                .ThenByDescending(i => i.Gen)
                .ThenBy(i => i.StudentNumber)
                .ToList();
            return Page();
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

            var activity = new Activity
            {
                Author = member,
            };
            if (!await TryUpdateModelAsync(activity, "Activity",
                i => i.Content,
                i => i.EndAt,
                i => i.Place,
                i => i.StartAt,
                i => i.Summary))
            {
                return BadRequest();
            }

            foreach (var attachment in Attachments)
            {
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(attachment.FileName)}";
                var path = Path.Combine(env.ContentRootPath, "storage", "attaches", filename);
                using (var fs = new FileStream(path, FileMode.CreateNew))
                {
                    await attachment.CopyToAsync(fs);
                }
                activity.Attachments.Add(new ActivityAttachment
                {
                    Filename = filename,
                    MediaType = attachment.ContentType,
                    OriginalFilename = attachment.FileName,
                    Size = attachment.Length,
                });
            }

            foreach (var attendeeStudentNumber in Request.Form["attend[]"].Select(uint.Parse))
            {
                activity.ActivityAttendees.Add(new ActivityAttendee
                {
                    AttandeeId = attendeeStudentNumber,
                });
            }

            await db.Activities.AddAsync(activity);
            await db.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                ActivityId = activity.Id,
            });
        }
    }
}