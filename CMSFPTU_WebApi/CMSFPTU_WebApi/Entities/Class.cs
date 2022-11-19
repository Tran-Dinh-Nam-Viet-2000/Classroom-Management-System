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
            ClassRooms = new HashSet<ClassRoom>();
            ClassSlots = new HashSet<ClassSlot>();
            ClassSubjects = new HashSet<ClassSubject>();
            Requests = new HashSet<Request>();
            Schedules = new HashSet<Schedule>();
        }

        public long ClassId { get; set; }
        public string ClassCode { get; set; }
        public int SystemStatusId { get; set; }

        public virtual SystemStatus SystemStatus { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ClassRoom> ClassRooms { get; set; }
        public virtual ICollection<ClassSlot> ClassSlots { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
