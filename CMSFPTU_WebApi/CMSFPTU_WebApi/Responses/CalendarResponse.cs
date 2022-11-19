using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class CalendarResponse
    {
        public long AccountId { get; set; }
        public long SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public long RoomId { get; set; }
        public int RoomNumber { get; set; }
        public Slot Slot { get; set; }
    }
}
