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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return accounts;
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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                }).FirstOrDefaultAsync(n => n.AccountId == id);
            if(getRecord == null || getRecord.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account does not exist"
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Success",
                    Body = getRecord
                };
            }
        }
        public async Task<ResponseApi> Create(AccountRequest account)
        {
            var checkAccountCode = _dbContext.Accounts.FirstOrDefault(n => n.AccountCode == account.AccountCode);
            var checkEmail = _dbContext.Accounts.FirstOrDefault(n => n.Email == account.Email);
            var createAccount = new Account
            {
                AccountCode = account.AccountCode,
                Email = account.Email,
                Firstname = account.Firstname,
                Lastname = account.Lastname,
                PasswordHash = Md5.MD5Hash(account.PasswordHash),
                RoleId = account.RoleId,
                SystemStatusId = account.SystemStatusId,
                Phone = account.Phone,
                Gender = account.Gender,
                CreatedAt = DateTime.Now,
            };
            if (checkAccountCode != null || checkEmail != null)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account code or email is existed"
                };
            }
            _dbContext.Add(createAccount);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Account added successfully",
                Body = createAccount
            };
        }
        public async Task<ResponseApi> Update(int id, AccountRequest updateAccount)
        {
            var queryAccount = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (queryAccount == null || queryAccount.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Not found account with id " + id,
                };
            }
            //var queryAccount = await _dbContext.Accounts.FirstOrDefaultAsync(n => n.SystemStatus.StatusCode == "Active");
            if (queryAccount != null)
            {
                queryAccount.AccountCode = updateAccount.AccountCode;
                queryAccount.Email = updateAccount.Email;
                queryAccount.Firstname = updateAccount.Firstname;
                queryAccount.Lastname = updateAccount.Lastname;
                queryAccount.PasswordHash = Md5.MD5Hash(updateAccount.PasswordHash);
                queryAccount.RoleId = updateAccount.RoleId;
                queryAccount.SystemStatusId = updateAccount.SystemStatusId;
                queryAccount.Phone = updateAccount.Phone;
                queryAccount.Gender = updateAccount.Gender;
                queryAccount.UpdatedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
            
            return new ResponseApi
            {
                Status = true,
                Message = "Update account is success",
                Body = queryAccount
            };
        }

        public async Task<ResponseApi> SoftDelete(int id)
        {
            var data = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (data == null || data.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account does not exist"
                };
            }
            data.SystemStatusId = (int)LkSystemStatus.Deleted;
            //_dbContext.Entry(data).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Delete account successfully",
            };
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var data = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (data == null)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account does not exist"
                };
            }
            data.SystemStatusId = (int)LkSystemStatus.Active;
            //_dbContext.Entry(data).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Restore account successfully",
            };
        }

        public async Task<IEnumerable<AccountResponse>> GetRecordDeleted()
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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return accounts;
        }

        public async Task<ResponseApi> GetRecordDeletedById(int id)
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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                }).FirstOrDefaultAsync(n => n.AccountId == id);
            if (getRecordDeleted == null || getRecordDeleted.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account does not exist"
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Success",
                    Body = getRecordDeleted
                };
            }
        }

        public async Task<ResponseApi> HardDelete(int id)
        {
            var data = _dbContext.Accounts.FirstOrDefault(n => n.AccountId == id);
            if (data == null || data.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Account does not exist"
                };
            }
            data.SystemStatusId = (int)LkSystemStatus.Deleted;
            _dbContext.Remove(data);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Hard deleted successfully"
            };
        }
    }
}
