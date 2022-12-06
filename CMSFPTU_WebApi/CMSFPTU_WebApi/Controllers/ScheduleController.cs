using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        [HttpGet]
        public async Task<IEnumerable<ScheduleResponse>> Get(int accountId)
        {
            var result = await _scheduleService.Get(accountId);
            return result; 
        }

        [HttpGet("get-for-admin")]
        public async Task<IEnumerable<ScheduleResponse>> GetForAdmin()
        {
            var result = await _scheduleService.GetForAdmin();
            return result;
        }

        [HttpGet("get-schedule")]
        public async Task<ResponseApi> GetSchedule(int scheduleId, int accountId)
        {
            var result = await _scheduleService.GetSchedule(scheduleId, accountId);
            return result;
        }

        [HttpPost]
        public async Task<ResponseApi> Create(ScheduleRequest scheduleRequest)
        {
            var result = await _scheduleService.Create(scheduleRequest);
            return result;
        }
    }
}
