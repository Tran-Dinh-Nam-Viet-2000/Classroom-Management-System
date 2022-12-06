using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Models;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using CMSFPTU_WebApi.Utils;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<AccountResponse>> Get()
        {
            var accounts = await _dbContext.Accounts
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).OrderByDescending(x => x.AccountId).ToListAsync();

            return accounts;
        }

        public async Task<IEnumerable<AccountResponse>> SearchAccount(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Accounts
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.AccountCode.ToLower().Contains(keyword)
                                                                            || n.Email.ToLower().Contains(keyword)
                                                                            || n.Firstname.ToLower().Contains(keyword)
                                                                            || n.Lastname.ToLower().Contains(keyword)
                                                                            || n.Phone.ToLower().Contains(keyword)
                                                                            || n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Role.RoleCode.ToLower().Contains(keyword)))
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<AccountResponse>> SearchAccountDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Accounts
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.AccountCode.ToLower().Contains(keyword)
                                                                            || n.Email.ToLower().Contains(keyword)
                                                                            || n.Firstname.ToLower().Contains(keyword)
                                                                            || n.Lastname.ToLower().Contains(keyword)
                                                                            || n.Phone.ToLower().Contains(keyword)
                                                                            || n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Role.RoleCode.ToLower().Contains(keyword)))
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetAccount(int id)
        {
            var getRecord = await _dbContext.Accounts
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).FirstOrDefaultAsync(n => n.AccountId == id);
            if(getRecord == null || getRecord.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = getRecord
                };
            }
        }
        public async Task<ResponseApi> Create(AccountRequest accountRequest)
        {
            if ("".Equals(accountRequest.AccountCode) || "".Equals(accountRequest.Firstname) || "".Equals(accountRequest.Lastname)
                || "".Equals(accountRequest.PasswordHash) || "".Equals(accountRequest.RoleId) || "".Equals(accountRequest.ClassId)
                || "".Equals(accountRequest.Phone) || "".Equals(accountRequest.Gender))
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail,
                };
            }
            var checkAccountCode = _dbContext.Accounts.FirstOrDefault(n => n.AccountCode == accountRequest.AccountCode);
            var checkEmail = _dbContext.Accounts.FirstOrDefault(n => n.Email == accountRequest.Email);
            var createAccount = new Account
            {
                AccountCode = accountRequest.AccountCode,
                Email = accountRequest.Email,
                Firstname = accountRequest.Firstname,
                Lastname = accountRequest.Lastname,
                PasswordHash = Md5.MD5Hash(accountRequest.PasswordHash),
                RoleId = accountRequest.RoleId,
                SystemStatusId = (int)LkSystemStatus.Active,
                ClassId = accountRequest.ClassId,
                Phone = accountRequest.Phone,
                Gender = accountRequest.Gender,
                CreatedAt = DateTime.Now,
            };
            if (checkAccountCode != null || checkEmail != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountAlreadyExists,
                };
            }
            _dbContext.Add(createAccount);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
                Body = createAccount
            };
        }
        public async Task<ResponseApi> Update(int id, UpdateAccountRequest updateAccountRequest)
        {
            var queryAccount = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (queryAccount == null || queryAccount.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull,
                };
            }
            else
            {
                queryAccount.AccountCode = updateAccountRequest.AccountCode;
                queryAccount.Email = updateAccountRequest.Email;
                queryAccount.Firstname = updateAccountRequest.Firstname;
                queryAccount.Lastname = updateAccountRequest.Lastname;
                queryAccount.PasswordHash = Md5.MD5Hash(updateAccountRequest.PasswordHash);
                queryAccount.RoleId = updateAccountRequest.RoleId;
                queryAccount.SystemStatusId = (int)LkSystemStatus.Active;
                queryAccount.Phone = updateAccountRequest.Phone;
                queryAccount.Gender = updateAccountRequest.Gender;
                queryAccount.UpdatedAt = DateTime.Now;
                queryAccount.ClassId = updateAccountRequest.ClassId;
                await _dbContext.SaveChangesAsync();
            }
            
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
                Body = queryAccount
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var data = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (data == null || data.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull,
                };
            }
            data.SystemStatusId = (int)LkSystemStatus.Deleted;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var data = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (data == null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull,
                };
            }
            data.SystemStatusId = (int)LkSystemStatus.Active;
            //_dbContext.Entry(data).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyRestored,
            };
        }

        public async Task<IEnumerable<AccountResponse>> GetDeleted()
        {
            var accounts = await _dbContext.Accounts
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).OrderByDescending(x => x.AccountId).ToListAsync();

            return accounts;
        }

        public async Task<ResponseApi> GetAccountDeleted(int id)
        {
            var getRecordDeleted = await _dbContext.Accounts
                .Select(n => new AccountResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    CreatedAt = n.CreatedAt,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    PasswordHash = n.PasswordHash,
                    Phone = n.Phone,
                    Role = n.Role,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    Class = n.Class,
                }).FirstOrDefaultAsync(n => n.AccountId == id);
            if (getRecordDeleted == null || getRecordDeleted.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = getRecordDeleted
                };
            }
        }
    }
}
