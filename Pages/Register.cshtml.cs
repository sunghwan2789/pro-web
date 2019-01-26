using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public uint StudentNumber { get; private set; }

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
        public int? PhoneNumber { get; set; }

        public void OnGet()
        {
            ViewData["Message"] = @"
                <p>�������� �̿��Ϸ��� ������ ��й�ȣ�� �����ؾ� �մϴ�.
                    ���� Ȯ���� ���� �ʿ��� ������ ��� �Է��ϰ� ���� ������ �Ϸ��ϼ���.
                <p>* ���� Ȯ���� �� �Ǹ� �����ڿ��� ���� �ٶ��ϴ�.";
        }
    }
}