using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Slot
    {
        public Slot()
        {
            Requests = new HashSet<Request>();
        }

        public long SlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
