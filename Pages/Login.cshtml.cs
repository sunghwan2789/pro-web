using System;
using System.Collections.Generic;
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

        [FromQuery, FromBody]
        public string ReturnUrl { get; set; }

        [FromBody]
        public string StudentNumber { get; set; }

        [FromBody]
        public string Password { get; set; }

        [FromBody]
        public bool Persist { get; set; }

        public void OnGet()
        {
        }

        public async void OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400;
                return;
            }

            var member = await db.Members.FindAsync(StudentNumber);
            if (member == null)
            {
                Response.StatusCode = 401;
                return;
            }
        }
    }
}