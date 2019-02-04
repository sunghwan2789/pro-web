using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromQuery]
        public string ReturnUrl { get; set; }

        [FromQuery]
        [Required]
        [DataType(DataType.Text)]
        public uint StudentNumber { get; set; }

        [FromForm]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [FromForm]
        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [FromForm]
        [Required]
        public string Name { get; set; }

        [FromForm]
        [Required]
        [DataType(DataType.Text)]
        public string PhoneNumber { get; set; }

        public void OnGet()
        {
            ViewData["Message"] = @"
                <p>�������� �̿��Ϸ��� ������ ��й�ȣ�� �����ؾ� �մϴ�.
                    ���� Ȯ���� ���� �ʿ��� ������ ��� �Է��ϰ� ���� ������ �Ϸ��ϼ���.
                <p>* ���� Ȯ���� �� �Ǹ� �����ڿ��� ���� �ٶ��ϴ�.";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != PasswordConfirm)
            {
                ViewData["Message"] = @"
                    <p>��й�ȣ�� �ٸ��ϴ�.";
                return Page();
            }

            var member = await db.Members.FindAsync(StudentNumber);
            if (
                member == null
                || member.Password != null
                || member.Name != Name
                || member.PhoneNumber != uint.Parse(Regex.Replace(PhoneNumber, "[^0-9]", ""))
            ) {
                ViewData["Message"] = $@"
                       <p>���� Ȯ���� �����Ͽ��ų� ��й�ȣ�� �̹� �����Ǿ� �ֽ��ϴ�.";
                return Page();
            }

            await db.MemberLogs.AddAsync(new Models.MemberLog
            {
                Member = member,
                Text = "INITIALIZE",
            });
            await db.SaveChangesAsync();

            member.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(Password, 12);
            await db.SaveChangesAsync();

            return RedirectToPage("./Login", new
            {
                Registered = true,
                ReturnUrl,
            });
        }
    }
}