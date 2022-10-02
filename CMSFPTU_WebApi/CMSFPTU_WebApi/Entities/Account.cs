using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Account
    {
        public Account()
        {
            Requests = new HashSet<Request>();
            Schedules = new HashSet<Schedule>();
        }

        public long AccountId { get; set; }
        public string AccountCode { get; set; }
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
        public DateTime? UpdatedAt { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool AccountStatus { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? EnrollmentYear { get; set; }
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
