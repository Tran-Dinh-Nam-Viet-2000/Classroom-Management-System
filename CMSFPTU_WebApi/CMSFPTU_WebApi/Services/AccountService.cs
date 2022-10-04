using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly CMSFPTUContext _dbContext;

        public AccountService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
