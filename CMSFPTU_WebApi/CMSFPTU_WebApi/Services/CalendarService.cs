using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly CMSFPTUContext _dbContext;

        public CalendarService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CalendarResponse>> Get(long accountId, DateTime fromDate, DateTime toDate)
        {
            var account = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == accountId);
            var querySubject = await (from classSubject in _dbContext.ClassSubjects
                               join classRoom in _dbContext.ClassRooms
                               on classSubject.ClassId equals classRoom.ClassId
                               join classSlot in _dbContext.ClassSlots
                               on classSubject.ClassId equals classSlot.ClassId
                               where classSubject.Class.Accounts.Contains(account) && DateTime.Compare((DateTime)classSubject.StartDate, fromDate) <= 0
                               && DateTime.Compare((DateTime)classSubject.EndDate, toDate) >= 0
                               select new CalendarResponse
                               {
                                   AccountId = accountId,
                                   RoomId = classRoom.RoomId,
                                   RoomNumber = classRoom.Room.RoomNumber,
                                   SubjectId = classSubject.SubjectId,
                                   SubjectCode = classSubject.Subject.SubjectCode,
                                   Slot = classSlot.Slot
                               }).ToListAsync();
            return querySubject;
        }
    }
}
