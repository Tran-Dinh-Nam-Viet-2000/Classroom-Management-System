using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ClassSubjectForRequestResponse
    {
        public long ClassId { get; set; }
        public Subject Subject { get; set; }
        public int? SystemStatusId { get; set; }
        public long ClassSubjectId { get; set; }
    }
}
