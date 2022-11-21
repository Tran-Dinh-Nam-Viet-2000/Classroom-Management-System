using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ClassSlotResponse
    {
        public long ClassSlotId { get; set; }
        public int SystemStatusId { get; set; }
        public Class Class { get; set; }
        public Slot Slot { get; set; }
    }
}
