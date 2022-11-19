using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface ICalendarService
    {
        Task<IEnumerable<CalendarResponse>> Get(long accountId, DateTime fromDate, DateTime toDate);
    }
}
