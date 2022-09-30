using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Student
    {
        public long StudentId { get; set; }
        public long AccountId { get; set; }
        public long ClassId { get; set; }
        public string StudentCode { get; set; }
        public bool StudentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual Class Class { get; set; }
    }
}
