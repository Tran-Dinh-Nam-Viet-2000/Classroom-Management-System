using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            Requests = new HashSet<Request>();
        }

        public long SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
