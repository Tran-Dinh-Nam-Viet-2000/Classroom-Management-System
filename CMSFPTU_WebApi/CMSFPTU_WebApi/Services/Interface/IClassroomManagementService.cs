using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IClassroomManagementService
    {
        Task<IEnumerable<ClassroomResponse>> Get(DateTime date, bool status, int slotId);
    }
}
