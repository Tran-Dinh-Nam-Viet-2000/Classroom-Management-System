using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class ScheduleRequest
    {
        public long ClassSubjectId { get; set; }
        public long RoomId { get; set; }
        public long SlotId { get; set; }
        public List<DateTime> ScheduleDates { get; set; }
    }
}
