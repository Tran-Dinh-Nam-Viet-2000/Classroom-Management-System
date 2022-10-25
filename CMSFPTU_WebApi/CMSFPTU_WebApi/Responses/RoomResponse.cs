using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class RoomResponse
    {
        public long RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int SystemStatusId { get; set; }
        public RoomType Type { get; set; }
    }
}
