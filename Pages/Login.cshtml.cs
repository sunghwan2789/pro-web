using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages
{
    public class LoginModel : PageModel
    {
        public LoginModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromQuery]
        public bool Registered { get; set; }

        [FromQuery]
        public string ReturnUrl { get; set; }

        [FromForm]
        [DataType(DataType.Text)]
        public uint? StudentNumber { get; set; }

        [FromForm]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [FromForm]
        public bool Persist { get; set; }

        public void OnGet()
        {
            if (Registered)
            {
                ViewData["Message"] = @"
                    <p>���� ������ �Ϸ��߽��ϴ�.
                    <p>�ٽ� �α����ϼ���.";
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var member = await db.Members.FindAsync(StudentNumber);
            if (member == null)
            {
                ViewData["Message"] = @"
                    <p>���̵� �����ϴ�.
                    <p>ȸ���̶�� �����ڿ��� �����ϼ���.";
                return Page();
            }

            if (member.Password == null)
            {
                return RedirectToPage("./Register", new
                {
                    ReturnUrl,
                    StudentNumber,
                });
            }

            // TODO: ��й�ȣ �˻�

            await db.MemberLogs.AddAsync(new Models.MemberLog
            {
                Member = member,
                Text = "LOGIN",
            });
            await db.SaveChangesAsync();

            // TODO: ���� �����

            return Redirect(ReturnUrl ?? $"{Request.PathBase}/");
        }
    }
}