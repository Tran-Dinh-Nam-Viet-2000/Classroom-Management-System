using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class ClassModel
    {
        public string ClassCode { get; set; }
        public int SystemStatusId { get; set; }

        public SystemStatus SystemStatus { get; set; }
    }
}
