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
            // �α����� ������ �α��� �õ��ϸ� ����
            if (HttpContext.Session.Keys.Contains("username"))
            {
                return Redirect(ReturnUrl ?? $"{Request.PathBase}/");
            }

            if (Registered)
            {
                ViewData["Message"] = @"
                    <p>���� ������ �Ϸ��߽��ϴ�.
                    <p>�ٽ� �α����ϼ���.";
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

            // ��й�ȣ�� �´��� �˻�
            if (!BCrypt.Net.BCrypt.EnhancedVerify(Password, member.Password))
            {
                if (!LegacyPasswordVerify(Password, member.Password))
                {
                    ViewData["Message"] = @"
                        <p>��й�ȣ�� Ʋ�Ƚ��ϴ�.
                        <p>��й�ȣ�� �ؾ��ٸ� �����ڿ��� �����ϼ���.";
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
