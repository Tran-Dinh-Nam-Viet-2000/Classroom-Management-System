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
                    AccountSubjectId = n.AccountSubjectId,
                    ClassId = (long)n.Account.ClassId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return accountSubjects;
        }

        public async Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubject(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.AccountSubjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.Account.AccountCode.ToLower().Contains(keyword)
                                                                            || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new AccountSubjectResponse
                {
                    AccountSubjectId = n.AccountSubjectId,
                    ClassId = (long)n.Account.ClassId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubjectDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.AccountSubjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.Account.AccountCode.ToLower().Contains(keyword)
                                                                             || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new AccountSubjectResponse
                {
                    AccountSubjectId = n.AccountSubjectId,
                    ClassId = (long)n.Account.ClassId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetAccountSubject(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountSubjectId = n.AccountSubjectId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
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

        public async Task<ResponseApi> Create(AccountSubjectRequest accountSubjectRequest)
        {            
            var accounts = _dbContext.Accounts.Where(n => n.ClassId == accountSubjectRequest.ClassId && n.RoleId == (int)LkRoles.Student).ToList();
            var checkClass = _dbContext.Accounts.FirstOrDefault(n => n.ClassId == accountSubjectRequest.ClassId);
            var checkSubject = _dbContext.Subjects.FirstOrDefault(n => n.SubjectId == accountSubjectRequest.SubjectId);
            if (accounts == null || checkClass == null || checkSubject == null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail,
                };
            }
            var accountSubjects = new List<AccountSubject>();
            accounts.ForEach(x => accountSubjects.Add(new AccountSubject
            {
                AccountId = x.AccountId,
                SubjectId = accountSubjectRequest.SubjectId,
                SystemStatusId = (int)LkSystemStatus.Active
            }));
            
            _dbContext.AccountSubjects.AddRange(accountSubjects);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };
        }

        public async Task<ResponseApi> Update(AccountSubjectRequest accountSubjectRequest)
        {
            var accounts = _dbContext.Accounts.Where(n => n.ClassId == accountSubjectRequest.ClassId
                                                       || n.RoleId == (int)LkRoles.Student)
                                              .Select(n => n.AccountId).ToList();
            var accountSubjects = _dbContext.AccountSubjects.Where(n => accounts.Contains(n.AccountId)).ToList();
            accountSubjects.ForEach(n => n.SubjectId = accountSubjectRequest.SubjectId);

            _dbContext.AccountSubjects.UpdateRange(accountSubjects);
            await _dbContext.SaveChangesAsync();
            
            return new ResponseApi
            {
                 Status = true,
                 Message = Messages.SuccessfullyUpdated,
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects.FirstOrDefaultAsync(n => n.AccountSubjectId == id);
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
                accountSubject.SystemStatusId = (int)LkSystemStatus.Deleted;
                await _dbContext.SaveChangesAsync();

                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.SuccessfullyDeleted,
                };
            }
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects.FirstOrDefaultAsync(n => n.AccountSubjectId == id);
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
                accountSubject.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();

                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.SuccessfullyRestored,
                };
            }
        }

        public async Task<IEnumerable<AccountSubjectResponse>> GetDeleted()
        {
            var accountSubjects = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountSubjectId = n.AccountSubjectId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return accountSubjects;
        }

        public async Task<ResponseApi> GetAccountSubjectDeleted(int id)
        {
            var accountSubject = await _dbContext.AccountSubjects
                .Select(n => new AccountSubjectResponse
                {
                    AccountSubjectId = n.AccountSubjectId,
                    AccountId = n.AccountId,
                    AccountCode = n.Account.AccountCode,
                    Subject = n.Subject,
                    SystemStatusId = n.SystemStatusId
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
