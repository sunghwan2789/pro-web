using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class TaskSource
    {
        public uint Id { get; set; }
        public uint TaskId { get; set; }
        public uint AuthorId { get; set; }
        public uint Sequence { get; set; }
        public DateTime SubmitAt { get; set; }
        public uint Size { get; set; }
        public int Status { get; set; }
        public int Score { get; set; }
        public string Error { get; set; }

        public virtual Task Task { get; set; }
        public virtual Member Author { get; set; }
    }
}
