using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class TeacherClass
    {
        public long TeacherId { get; set; }
        public long ClassId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
