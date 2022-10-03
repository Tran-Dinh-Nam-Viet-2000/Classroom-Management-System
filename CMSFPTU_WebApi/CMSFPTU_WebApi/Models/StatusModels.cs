using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class StatusModels
    {
        public int SystemStatusId { get; set; }
        public string StatusCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
