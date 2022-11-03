using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class RequestTeacherModel
    {
        public long RequestId { get; set; }
        public long AccountId { get; set; }
        public string RequestName { get; set; }
        public string RequestDescription { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequestTime { get; set; }
        public long RequestBy { get; set; }
        public long SubjectId { get; set; }
        public long SlotId { get; set; }
        public long RoomId { get; set; }
        public long ClassId { get; set; }
        public int SystemStatusId { get; set; }

        public Account Account { get; set; }
        public Class Class { get; set; }
        public Room Room { get; set; }
        public Slot Slot { get; set; }
        public Subject Subject { get; set; }
    }
}
