using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Schedule
    {
        public long ScheduleId { get; set; }
        public long AccountId { get; set; }
        public long ClassId { get; set; }
        public long RoomId { get; set; }
        public long SlotId { get; set; }
        public DateTime ScheduleDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual Class Class { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
    }
}
