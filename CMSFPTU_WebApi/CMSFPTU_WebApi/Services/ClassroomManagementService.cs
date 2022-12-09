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
        public async Task<IEnumerable<ClassroomResponse>> Get(DateTime date, bool status,int slotId)
        {
            var rooms = _dbContext.Rooms.Include(x => x.Type).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToList();
            var query = _dbContext.Schedules;
            var schedule = _dbContext.Schedules.Where(n => n.ScheduleDate == date).ToList();
            if (status)
            { 
                return rooms.Where(n => !schedule.Where(x => x.SlotId == slotId).Select(x => x.RoomId).Contains(n.RoomId))
                    .Select(n => new ClassroomResponse
                    {
                        RoomType = n.Type,
                        RoomId = n.RoomId,
                        RoomNumber = n.RoomNumber,
                    });
            }
            else
            {
                return await query.Where(n => n.ScheduleDate == date && n.SlotId == slotId && rooms.Contains(n.Room))
                    .Select(n => new ClassroomResponse
                    {
                        Date = n.ScheduleDate,
                        RoomType = n.Room.Type,
                        RoomNumber = n.Room.RoomNumber,
                        Slot = n.Slot
                    }).ToListAsync();
            }
        }
    }
}
