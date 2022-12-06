using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ScheduleDetailsResponse
    {
        public long ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int SystemStatusId { get; set; }
        public ClassSubject ClassSubject { get; set; }
        public Room Room { get; set; }
        public Slot Slot { get; set; }
    }
}
