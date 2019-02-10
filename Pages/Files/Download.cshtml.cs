using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pro_web.Pages.Files
{
    public class DownloadModel : PageModel
    {
        public DownloadModel(ProContext db, IHostingEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        private readonly ProContext db;
        private readonly IHostingEnvironment env;

        public async Task<IActionResult> OnGetAsync(uint fileId)
        {
            var file = await db.ActivityAttachments.FindAsync(fileId);
            if (file == null)
            {
                return NotFound();
            }

            var path = Path.Combine(env.ContentRootPath, "storage", "attaches", file.Filename);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(stream, file.MediaType, file.OriginalFilename);
        }
    }
}