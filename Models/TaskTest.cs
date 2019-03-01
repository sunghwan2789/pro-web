using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro_web.Models
{
    public partial class TaskTest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public int Score { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Input { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Output { get; set; }

        public virtual Task Task { get; set; }
    }
}
