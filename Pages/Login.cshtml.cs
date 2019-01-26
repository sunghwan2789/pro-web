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
                    <p>계정 설정을 완료했습니다.
                    <p>다시 로그인하세요.";
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
                    <p>아이디가 없습니다.
                    <p>회원이라면 관리자에게 문의하세요.";
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

            // TODO: 비밀번호 검사

            await db.MemberLogs.AddAsync(new Models.MemberLog
            {
                Member = member,
                Text = "LOGIN",
            });
            await db.SaveChangesAsync();

            // TODO: 세션 만들기

            return Redirect(ReturnUrl ?? $"{Request.PathBase}/");
        }
    }
}