using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Request
    {
        public long RequestId { get; set; }
        public long AccountId { get; set; }
        public string RequestName { get; set; }
        public string RequestDescription { get; set; }
        public bool RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RequestTime { get; set; }
        public long RequestBy { get; set; }
        public long SubjectId { get; set; }
        public long SlotId { get; set; }
        public long RoomId { get; set; }
        public long ClassId { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }

        public virtual Account Account { get; set; }
        public virtual Class Class { get; set; }
        public virtual Room Room { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
