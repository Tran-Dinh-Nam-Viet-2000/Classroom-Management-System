using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class AccountModel
    {
        public string AccountCode { get; set; }
        public string PasswordHash { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long RoleId { get; set; }
        public string RoleCode { get; set; }
        public int SystemStatusId { get; set; }
        public string StatusCode { get; set; }
        public SystemStatus SystemStatus { get; set; }
        public Role Role { get; set; }
    }
}
