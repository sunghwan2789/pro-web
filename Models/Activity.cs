﻿using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Attachments = new HashSet<ActivityAttachment>();
            ActivityAttendees = new HashSet<ActivityAttendee>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Place { get; set; }

        public virtual Member Author { get; set; }
        public virtual ICollection<ActivityAttachment> Attachments { get; set; }
        public virtual ICollection<ActivityAttendee> ActivityAttendees { get; set; }
    }
}
