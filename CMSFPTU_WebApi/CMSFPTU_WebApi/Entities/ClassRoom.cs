using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class ClassRoom
    {
        public long ClassRoomId { get; set; }
        public long ClassId { get; set; }
        public long RoomId { get; set; }
        public int SystemStatusId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Room Room { get; set; }
        public virtual SystemStatus SystemStatus { get; set; }
    }
}
