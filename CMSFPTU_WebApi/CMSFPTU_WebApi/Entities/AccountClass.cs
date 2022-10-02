using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class AccountClass
    {
        public long AccountId { get; set; }
        public long ClassId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Class Class { get; set; }
    }
}
