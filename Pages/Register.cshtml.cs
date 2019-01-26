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
                <p>누리집을 이용하려면 계정에 비밀번호를 설정해야 합니다.
                    본인 확인을 위해 필요한 정보를 모두 입력하고 계정 설정을 완료하세요.
                <p>* 본인 확인이 안 되면 관리자에게 문의 바랍니다.";
        }
    }
}