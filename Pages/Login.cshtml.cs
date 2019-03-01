using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public ActionResult OnGet()
        {
            // 로그인한 유저가 로그인 시도하면 차단
            if (HttpContext.Session.Keys.Contains("username"))
            {
                return Redirect(ReturnUrl ?? $"{Request.PathBase}/");
            }

            if (Registered)
            {
                ViewData["Message"] = @"
                    <p>계정 설정을 완료했습니다.
                    <p>다시 로그인하세요.";
            }
            return Page();
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

            // 비밀번호가 맞는지 검사
            if (!BCrypt.Net.BCrypt.EnhancedVerify(Password, member.Password))
            {
                if (!LegacyPasswordVerify(Password, member.Password))
                {
                    ViewData["Message"] = @"
                        <p>비밀번호가 틀렸습니다.
                        <p>비밀번호를 잊었다면 관리자에게 문의하세요.";
                    return Page();
                }
            }
            if (BCrypt.Net.BCrypt.PasswordNeedsRehash(member.Password, 12))
            {
                member.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 12);
                await db.SaveChangesAsync();
            }

            db.MemberLogs.Add(new Models.MemberLog
            {
                Member = member,
                Content = "LOGIN",
            });
            await db.SaveChangesAsync();

            await HttpContext.Session.LoadAsync();
            HttpContext.Session.SetInt32("username", member.Id);
            await HttpContext.Session.CommitAsync();

            return Redirect(ReturnUrl ?? $"{Request.PathBase}/");
        }

        private bool LegacyPasswordVerify(string text, string hash)
        {
            string sha1(string s) => string.Join("", SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(s)).Select(i => i.ToString("x2")));
            const string legacySalt = "y#.fij/|lP&!79.Txcf]";
            var p = sha1(legacySalt + sha1(text + legacySalt) + text);
            return BCrypt.Net.BCrypt.Verify(p, hash);
        }
    }
}
