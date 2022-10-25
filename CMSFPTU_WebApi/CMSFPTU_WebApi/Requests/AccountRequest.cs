using System;

namespace CMSFPTU_WebApi.Requests
{
    public class AccountRequest
    {
        public string AccountCode { get; set; }
        public string PasswordHash { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public long RoleId { get; set; }
        public long? ClassId { get; set; }
    }
}
