using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class RoomRequest
    {
        public int RoomNumber { get; set; }
        public long TypeId { get; set; }
    }
}
