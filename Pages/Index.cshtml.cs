using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pro_web.Models;

namespace pro_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProContext context;

        public IndexModel(ProContext context)
        {
            this.context = context;
        }

        public IEnumerable<Activity> Activities => context.Activities;
        public IEnumerable<Member> Members => context.Members;

        public void OnGet()
        {
        }
    }
}