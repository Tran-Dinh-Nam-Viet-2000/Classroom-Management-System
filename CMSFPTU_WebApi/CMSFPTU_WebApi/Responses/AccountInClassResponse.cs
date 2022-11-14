using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class AccountInClassResponse
    {
        public long AccountId { get; set; }
        public string AccountCode { get; set; }
        public long ClassId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int SystemStatusId { get; set; }
        public long RoleId { get; set; }
    }
}
