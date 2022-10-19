using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class RoomTypeRequest
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}

