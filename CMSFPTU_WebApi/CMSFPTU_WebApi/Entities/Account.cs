using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Account
    {
        public Account()
        {
            Admins = new HashSet<Admin>();
            Requests = new HashSet<Request>();
            Students = new HashSet<Student>();
            Teachers = new HashSet<Teacher>();
        }

        public long AccountId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
