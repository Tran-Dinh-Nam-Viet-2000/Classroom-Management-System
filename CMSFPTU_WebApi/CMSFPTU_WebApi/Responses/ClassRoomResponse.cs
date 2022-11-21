using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ClassRoomResponse
    {
        public long ClassRoomId { get; set; }
        public Class Class { get; set; }
        public Room Room { get; set; }
        public int SystemStatusId { get; set; }
    }
}
