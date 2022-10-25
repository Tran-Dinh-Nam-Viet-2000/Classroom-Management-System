﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class AccountSubject
    {
        public long AccountId { get; set; }
        public long SubjectId { get; set; }
        public long AccountSubjectId { get; set; }
        public int SystemStatusId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual SystemStatus SystemStatus { get; set; }
    }
}
