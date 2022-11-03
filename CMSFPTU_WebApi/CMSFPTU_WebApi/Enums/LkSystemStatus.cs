using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Enums
{
    public enum LkSystemStatus
    {
        Active = 1,
        Deleted = 2,
        WaitingForApproval = 3,
        Approved = 4,
        Rejected = 5
    }
}
