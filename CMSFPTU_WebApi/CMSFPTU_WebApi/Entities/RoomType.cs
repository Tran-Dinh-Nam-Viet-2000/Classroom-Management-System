using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public long TypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int? SystemStatusId { get; set; }

        public virtual SystemStatus SystemStatus { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
