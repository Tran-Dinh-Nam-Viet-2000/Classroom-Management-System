using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Teacher
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
        }

        public long TeacherId { get; set; }
        public long AccountId { get; set; }
        public string TeacherCode { get; set; }
        public bool TeacherStatus { get; set; }
        public DateTime HiringDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
