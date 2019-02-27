using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro_web.Models
{
    public partial class TaskTest
    {
        public uint Id { get; set; }
        public uint TaskId { get; set; }

        public uint Score { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Input { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Output { get; set; }

        public virtual Task Task { get; set; }
    }
}
