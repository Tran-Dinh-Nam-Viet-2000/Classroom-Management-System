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
            var query = _dbContext.Schedules;
            if (status)
            {
                query.Where(n => n.ScheduleDate != date && n.SystemStatusId != (int)LkSystemStatus.Deleted);
            }
            else
            {
                query.Where(n => n.ScheduleDate == date && n.SystemStatusId == (int)LkSystemStatus.Active);
            }


            return await query.Select(n => new ClassroomResponse { 
                Date = n.ScheduleDate,
                RoomNumber = n.Room.RoomNumber,
                SlotId = n.SlotId
            }).ToListAsync();
        }
    }
}
