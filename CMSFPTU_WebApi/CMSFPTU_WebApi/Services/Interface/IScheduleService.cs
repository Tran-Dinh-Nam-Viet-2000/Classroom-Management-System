using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleResponse>> Get(int accountId);
        Task<ResponseApi> GetSchedule(int scheduleId, int accountId);
        Task<IEnumerable<ScheduleResponse>> GetForAdmin();
        Task<ResponseApi> Create(ScheduleRequest scheduleRequest);
        //Task<ResponseApi> Delete(int id);
    }
}
