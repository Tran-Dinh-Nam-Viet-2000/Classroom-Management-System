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
        }

        public int SystemStatusId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
