using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class AccountModels
    {
        public string AccountCode { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public SystemStatus SystemStatus { get; set; }
        public Role Role { get; set; }
    }
}
