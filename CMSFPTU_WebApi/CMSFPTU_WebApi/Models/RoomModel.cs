using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class RoomModel
    {
        public int RoomNumber { get; set; }
        public int SystemStatusId { get; set; }
        public long TypeId { get; set; }
        public RoomType Type { get; set; }
        public SystemStatus SystemStatus { get; set; }
    }
}
