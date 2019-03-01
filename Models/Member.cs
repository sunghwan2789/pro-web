using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
            Submissions = new HashSet<Submission>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Gen { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public int Authority { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ActivityAttendee> ActivityAttendees { get; set; }
        public virtual ICollection<MemberHistory> MemberHistory { get; set; }
        public virtual ICollection<MemberLog> MemberLogs { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
