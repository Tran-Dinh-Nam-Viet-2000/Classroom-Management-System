using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Models;
using CMSFPTU_WebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class StatusService : IStatusServices
    {
        private readonly CMSFPTUContext _dbContext;

        public StatusService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<StatusModels> GetAll()
        {
            var result = _dbContext.SystemStatuses.Where(n => n.Status == true).Select(n => new StatusModels
            {
                SystemStatusId = n.SystemStatusId,
                StatusCode = n.StatusCode,
                CreatedDate = n.CreatedDate
            }).ToList();
            return result;
        }
    }
}
