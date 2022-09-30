using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class Admin
    {
        public long AdminId { get; set; }
        public long AccountId { get; set; }
        public string AdminCode { get; set; }
        public bool AdminStatus { get; set; }
        public DateTime HiringDate { get; set; }

        public virtual Account Account { get; set; }
    }
}
