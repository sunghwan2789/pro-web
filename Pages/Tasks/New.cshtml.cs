using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Tasks
{
    public class NewModel : PageModel
    {
        public NewModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [BindProperty]
        public Models.Task Task { get; set; }

        public void OnGet()
        {
            // TODO: 권한 확인
        }
    }
}