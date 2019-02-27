using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class Task
    {
        public Task()
        {
            Sources = new HashSet<Submission>();
            Tests = new HashSet<TaskTest>();
        }

        public uint Id { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ExampleInput { get; set; }

        public string ExampleOutput { get; set; }

        public sbyte Hidden { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Submission> Sources { get; set; }
        public virtual ICollection<TaskTest> Tests { get; set; }
    }
}
