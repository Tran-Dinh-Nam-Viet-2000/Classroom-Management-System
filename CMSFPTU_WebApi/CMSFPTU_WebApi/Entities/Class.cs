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
            Students = new HashSet<Student>();
        }

        public long ClassId { get; set; }
        public long TeacherId { get; set; }
        public string ClassCode { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
