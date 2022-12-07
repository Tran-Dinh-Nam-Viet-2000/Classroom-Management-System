using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class ClassroomManagementService : IClassroomManagementService
    {
        private readonly CMSFPTUContext _dbContext;

        public ClassroomManagementService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClassroomResponse>> Get(DateTime date, bool status)
        {
            var rooms = _dbContext.Rooms.Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToList();
            var query = _dbContext.Schedules;
            if (status)
            {
                return await query.Where(n => n.ScheduleDate == date && !rooms.Contains(n.Room))
                    .Select(n => new ClassroomResponse
                    {
                        Date = n.ScheduleDate,
                        RoomNumber = n.Room.RoomNumber,
                        SlotId = n.SlotId
                    }).ToListAsync();
            }
            else
            {
                return await query.Where(n => n.ScheduleDate == date && rooms.Contains(n.Room))
                    .Select(n => new ClassroomResponse
                    {
                        Date = n.ScheduleDate,
                        RoomNumber = n.Room.RoomNumber,
                        SlotId = n.SlotId
                    }).ToListAsync();
            }
        }
    }
}
