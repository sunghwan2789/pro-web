using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Models
{
    public partial class ActivityAttachment
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }

        [Required]
        public string Filename { get; set; }

        public string OriginalFilename { get; set; }

        public string MediaType { get; set; }

        public long Size { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
