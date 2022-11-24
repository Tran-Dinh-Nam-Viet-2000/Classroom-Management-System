using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class ClassSubject
    {
        public ClassSubject()
        {
            Schedules = new HashSet<Schedule>();
        }

        public long ClassId { get; set; }
        public long SubjectId { get; set; }
        public int? SystemStatusId { get; set; }
        public long ClassSubjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SystemStatus SystemStatus { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
