using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class AccountSubjectService : IAccountSubjectService
    {
        private readonly CMSFPTUContext _dbContext;

        public AccountSubjectService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AccountSubjectResponse>> Get()
        {
            var accountSubjects = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountId = n.AccountId,
                    SubjectId = n.SubjectId,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return accountSubjects;
        }
        
        public async Task<ResponseApi> GetAccountSubject(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountId = n.AccountId,
                    SubjectId = n.SubjectId
                }).FirstOrDefaultAsync(n => n.AccountSubjectId == id);
            if (accountSubject == null || accountSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = accountSubject
                };
            }
        }

        //public async Task<ResponseApi> Create(AccountSubjectRequest accountSubjectRequest)
        //{
        //    var status = (int)LkSystemStatus.Active;
        //    var create = new AccountSubject
        //    {
        //        AccountId = accountSubjectRequest.AccountId,
        //        SubjectId = accountSubjectRequest.SubjectId,
        //        SystemStatusId = status
        //    };
        //    _dbContext.Add(create);
        //    await _dbContext.SaveChangesAsync();

        //    return new ResponseApi
        //    {
        //        Status = true,
        //        Message = Messages.SuccessfullyAddedNew,
        //    };
        //}

        //public async Task<ResponseApi> Update(int id, AccountSubjectRequest accountSubjectRequest)
        //{
        //    var accountSubject = await _dbContext.AccountSubjects.FirstOrDefaultAsync(n => n.AccountSubjectId == id);
        //    var status = (int)LkSystemStatus.Active;
        //    if (accountSubject == null || accountSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
        //    {
        //        return new ResponseApi
        //        {
        //            Status = false,
        //            Message = Messages.ClassIsNull,
        //        };
        //    }
        //    else
        //    {
        //        accountSubject.AccountId = accountSubjectRequest.AccountId;
        //        accountSubject.SubjectId = accountSubjectRequest.SubjectId;
        //        accountSubject.SystemStatusId = status;
        //        await _dbContext.SaveChangesAsync();

        //        return new ResponseApi
        //        {
        //            Status = true,
        //            Message = Messages.SuccessfullyUpdated,
        //        };
        //    }
        //}

        //public async Task<ResponseApi> Delete(int id)
        //{
        //    var accountSubject = await _dbContext.AccountSubjects.FirstOrDefaultAsync(n => n.AccountSubjectId == id);
        //    if (accountSubject == null || accountSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
        //    {
        //        return new ResponseApi
        //        {
        //            Status = false,
        //            Message = Messages.RecordIsNull,
        //        };
        //    }
        //    else
        //    {
        //        accountSubject.SystemStatusId = (int)LkSystemStatus.Deleted;
        //        await _dbContext.SaveChangesAsync();

        //        return new ResponseApi
        //        {
        //            Status = true,
        //            Message = Messages.SuccessfullyDeleted,
        //        };
        //    }
        //}

        //public async Task<ResponseApi> Restore(int id)
        //{
        //    var accountSubject = await _dbContext.AccountSubjects.FirstOrDefaultAsync(n => n.AccountSubjectId == id);
        //    if (accountSubject == null || accountSubject.SystemStatusId == (int)LkSystemStatus.Active)
        //    {
        //        return new ResponseApi
        //        {
        //            Status = false,
        //            Message = Messages.RecordIsNull,
        //        };
        //    }
        //    else
        //    {
        //        accountSubject.SystemStatusId = (int)LkSystemStatus.Active;
        //        await _dbContext.SaveChangesAsync();

        //        return new ResponseApi
        //        {
        //            Status = true,
        //            Message = Messages.SuccessfullyRestored,
        //        };
        //    }
        //}

        public async Task<IEnumerable<AccountSubjectResponse>> GetDeleted()
        {
            var accountSubjects = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountId = n.AccountId,
                    SubjectId = n.SubjectId,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return accountSubjects;
        }

        public async Task<ResponseApi> GetAccountDeleted(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountId = n.AccountId,
                    SubjectId = n.SubjectId
                }).FirstOrDefaultAsync(n => n.AccountSubjectId == id);
            if (accountSubject == null || accountSubject.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = accountSubject
                };
            }
        }
    }
}
