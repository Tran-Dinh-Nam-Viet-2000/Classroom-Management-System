using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class FilterClassResponse
    {
        public long ClassId { get; set; }
        public long AccountId { get; set; }
        public string ClassCode { get; set; }
        public int SystemStatusId { get; set; }

    }
}
