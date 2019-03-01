using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro_web.Models
{
    public partial class Submission
    {
        public enum StatusCode
        {
            SuccessOrInitialization = 0,
            CompilationError = 1,
            RuntimeError = 2,
            PartialSuccess = 3,
        }

        public int Id { get; set; }
        public int TaskId { get; set; }
        public int AuthorId { get; set; }

        public int Sequence { get; set; }

        public DateTime SubmitAt { get; set; }

        public long Size { get; set; }

        public StatusCode Status { get; set; }

        public int Score { get; set; }

        public string Error { get; set; }

        public bool Working { get; set; }

        public CompileAndGo.Languages Language { get; set; }

        [Required]
        public string Filename { get; set; }

        public virtual Task Task { get; set; }
        public virtual Member Author { get; set; }
    }
}
