using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Room
    {
        public Room()
        {
            Requests = new HashSet<Request>();
        }

        public long RoomId { get; set; }
        public int RoomNumber { get; set; }
        public bool RoomStatus { get; set; }
        public long TypeId { get; set; }

        public virtual RoomType Type { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
