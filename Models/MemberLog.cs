using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class MemberLog
    {
        public int Id { get; set; }
        public DateTime OccurredAt { get; set; }
        public int MemberId { get; set; }
        public string Text { get; set; }

        public virtual Member Member { get; set; }
    }
}
