using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class MemberHistory
    {
        public uint Id { get; set; }
        public uint MemberId { get; set; }
        public string Content { get; set; }
        public DateTime OccurredAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
