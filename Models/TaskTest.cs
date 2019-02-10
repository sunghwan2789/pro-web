using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class TaskTest
    {
        public uint Id { get; set; }
        public uint TaskId { get; set; }
        public uint Score { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public virtual Task Task { get; set; }
    }
}
