using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class AccountSubjectResponse
    {
        public long AccountSubjectId { get; set; }
        public long AccountId { get; set; }
        public long SubjectId { get; set; }
        public int SystemStatusId { get; set; }
    }
}
