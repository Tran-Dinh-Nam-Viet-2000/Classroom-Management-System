using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class RequestTeacherRequest
    {
        public long RequestTypeId { get; set; }
        public DateTime RequestDate { get; set; }
        public long SubjectId { get; set; }
        public long SlotId { get; set; }
        public long RoomId { get; set; }
        public long ClassId { get; set; }
        public long AccountId { get; set; }
    }
}
