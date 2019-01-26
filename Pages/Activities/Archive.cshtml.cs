using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Activities
{
    public class ArchiveModel : PageModel
    {
        public ArchiveModel(ProContext db)
        {
            this.db = db;
        }

        private readonly ProContext db;

        [FromRoute]
        public int? Year { get; set; } = DateTime.Now.Year;

        [FromRoute]
        public int? Month { get; set; }

        public ICollection<Models.Activity> Activities { get; set; }

        public void OnGet()
        {
            var startDate = DateTime.MinValue;
            var endDate = DateTime.Today;
            if (Year != null)
            {
                startDate = new DateTime(Year.Value, 1, 1);
                endDate = startDate.AddYears(1).AddDays(-1);
                if (Month != null)
                {
                    startDate = new DateTime(Year.Value, Month.Value, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                }
            }

            Activities = db.Activities.Where(i =>
                i.StartAt.Date >= startDate
                && i.EndAt.Date <= endDate
            )
            .OrderByDescending(i => i.StartAt)
            .ToList();
        }
    }
}