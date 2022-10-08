using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class AccountSubject
    {
        public long AccountId { get; set; }
        public long SubjectId { get; set; }
        public long AccountSubjectId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
