using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public class ScheduleService : IScheduleService
    {
        private readonly CMSFPTUContext _dbContext;

        public ScheduleService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseApi> Create(ScheduleRequest scheduleRequest)
        {
            var query = await _dbContext.Schedules.FirstOrDefaultAsync(n => n.ClassSubjectId == scheduleRequest.ClassSubjectId && n.RoomId == scheduleRequest.RoomId
                                                                         && n.SlotId == scheduleRequest.SlotId);
            if (query != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail
                }; 
            }
            var schedules = new List<Schedule>();
            scheduleRequest.ScheduleDates.ForEach(x => schedules.Add(new Schedule
            {
                ClassSubjectId = scheduleRequest.ClassSubjectId,
                RoomId = scheduleRequest.RoomId,
                SlotId = scheduleRequest.SlotId,
                ScheduleDate = x
            }));

            await _dbContext.Schedules.AddRangeAsync(schedules);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew
            };
        }

        public async Task<IEnumerable<ScheduleResponse>> Get(int accountId)
        {
            var schedule = await _dbContext.Schedules
                .Select(n => new ScheduleResponse
                {
                    ClassSubject = n.ClassSubject,
                    Room = n.Room,
                    ScheduleId = n.ScheduleId,
                    StartTime = new DateTime(n.ScheduleDate.Year, n.ScheduleDate.Month, n.ScheduleDate.Day, n.Slot.StartTime.Hours, n.Slot.StartTime.Minutes, n.Slot.StartTime.Seconds) ,
                    EndTime = new DateTime(n.ScheduleDate.Year, n.ScheduleDate.Month, n.ScheduleDate.Day, n.Slot.EndTime.Hours, n.Slot.EndTime.Minutes, n.Slot.EndTime.Seconds),
                    Slot = n.Slot
                }).Where(x => x.ClassSubject.Class.Accounts.Select(x =>x.AccountId).Contains(accountId)).ToListAsync();
            return schedule;
        }
    }
}
