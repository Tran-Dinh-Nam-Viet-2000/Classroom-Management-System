using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Class
    {
        public Class()
        {
            Requests = new HashSet<Request>();
            Schedules = new HashSet<Schedule>();
        }

        public long ClassId { get; set; }
        public string ClassCode { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
