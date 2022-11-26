using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Schedule
    {
        public long ScheduleId { get; set; }
        public long ClassSubjectId { get; set; }
        public long RoomId { get; set; }
        public long SlotId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int? SystemStatusId { get; set; }

        public virtual ClassSubject ClassSubject { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual SystemStatus SystemStatus { get; set; }
    }
}
