using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages
{
    [IgnoreAntiforgeryToken]
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            RedirectToPage("/Index");
        }

        public async Task OnPostAsync()
        {
            HttpContext.Session.Clear();
            await HttpContext.Session.CommitAsync();
        }
    }
}