﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_web.Models
{
    public partial class ActivityAttendee
    {
        public uint ActivityId { get; set; }
        public uint AttandeeId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Member Attendee { get; set; }
    }
}
