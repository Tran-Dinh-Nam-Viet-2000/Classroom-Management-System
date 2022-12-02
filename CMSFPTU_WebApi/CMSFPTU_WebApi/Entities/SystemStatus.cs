using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class SystemStatus
    {
        public SystemStatus()
        {
            Accounts = new HashSet<Account>();
            ClassSubjects = new HashSet<ClassSubject>();
            Classes = new HashSet<Class>();
            Requests = new HashSet<Request>();
            RoomTypes = new HashSet<RoomType>();
            Rooms = new HashSet<Room>();
            Schedules = new HashSet<Schedule>();
            Subjects = new HashSet<Subject>();
        }

        public int SystemStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
