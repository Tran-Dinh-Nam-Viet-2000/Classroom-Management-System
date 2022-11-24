using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ClassSubjectResponse
    {
        public long ClassSubjectId { get; set; }
        public long AccountId { get; set; }
        public long ClassId { get; set; }
        public string ClassCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Subject Subject { get; set; }
        public int SystemStatusId { get; set; }
    }
}
