using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Class
    {
        public Class()
        {
            Accounts = new HashSet<Account>();
            ClassSubjects = new HashSet<ClassSubject>();
            Requests = new HashSet<Request>();
        }

        public long ClassId { get; set; }
        public string ClassCode { get; set; }
        public int SystemStatusId { get; set; }

        public virtual SystemStatus SystemStatus { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
