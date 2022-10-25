using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class AccountSubjectRequest
    {
        public long AccountId { get; set; }
        public long SubjectId { get; set; }
    }
}
