using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class MemberHistory
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        public string Content { get; set; }
        public DateTime OccurredAt { get; set; }

        public virtual Member Member { get; set; }
    }
}
