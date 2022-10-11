﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class SystemStatus
    {
        public SystemStatus()
        {
            Accounts = new HashSet<Account>();
            Classes = new HashSet<Class>();
            Rooms = new HashSet<Room>();
            Subjects = new HashSet<Subject>();
        }

        public int SystemStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
