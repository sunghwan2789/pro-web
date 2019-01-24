using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Models
{
    public partial class ActivityAttachment
    {
        public uint Id { get; set; }
        public uint ActivityId { get; set; }
        public string Filename { get; set; }
        public string OriginalFilename { get; set; }

        public virtual Activity Activity { get; set; }
    }
}
