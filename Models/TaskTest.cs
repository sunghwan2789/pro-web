using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class TaskTest
    {
        public uint Id { get; set; }
        public uint TaskId { get; set; }
        public uint Score { get; set; }
        public byte[] Input { get; set; }
        public byte[] Output { get; set; }

        public virtual Task Task { get; set; }
    }
}
