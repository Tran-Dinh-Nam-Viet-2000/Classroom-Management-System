using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ResponseApi
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Object Body { get; set; }
    }
}
