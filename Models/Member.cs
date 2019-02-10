using System;
using System.Collections.Generic;

namespace pro_web.Models
{
    public partial class Member
    {
        public Member()
        {
            Activities = new HashSet<Activity>();
            ActivityAttendees = new HashSet<ActivityAttendee>();
            MemberHistory = new HashSet<MemberHistory>();
            MemberLogs = new HashSet<MemberLog>();
            TaskSources = new HashSet<TaskSource>();
        }

        public byte Gen { get; set; }
        public uint StudentNumber { get; set; }
        public string Name { get; set; }
        public uint PhoneNumber { get; set; }
        public byte Authority { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ActivityAttendee> ActivityAttendees { get; set; }
        public virtual ICollection<MemberHistory> MemberHistory { get; set; }
        public virtual ICollection<MemberLog> MemberLogs { get; set; }
        public virtual ICollection<TaskSource> TaskSources { get; set; }
    }
}
