using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class ClassSlot
    {
        public long ClassSlotId { get; set; }
        public long ClassId { get; set; }
        public long SlotId { get; set; }
        public int SystemStatusId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual SystemStatus SystemStatus { get; set; }
    }
}
