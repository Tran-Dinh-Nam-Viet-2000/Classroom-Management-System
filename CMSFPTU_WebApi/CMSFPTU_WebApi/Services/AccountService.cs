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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    ClassId = n.ClassId,
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
                    ClassId = n.ClassId,
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
        public async Task<ResponseApi> Create(AccountRequest account)
        {
            var checkAccountCode = _dbContext.Accounts.FirstOrDefault(n => n.AccountCode == account.AccountCode);
            var checkEmail = _dbContext.Accounts.FirstOrDefault(n => n.Email == account.Email);
            var statusIsActive = 1;
            var createAccount = new Account
            {
                AccountCode = account.AccountCode,
                Email = account.Email,
                Firstname = account.Firstname,
                Lastname = account.Lastname,
                PasswordHash = Md5.MD5Hash(account.PasswordHash),
                RoleId = account.RoleId,
                SystemStatusId = statusIsActive,
                ClassId = account.ClassId,
                Phone = account.Phone,
                Gender = account.Gender,
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
            var statusIsActive = 1;
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
                queryAccount.SystemStatusId = statusIsActive;
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
            //_dbContext.Entry(data).State = EntityState.Modified;
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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    ClassId = n.ClassId,
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

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
                    RoleId = n.RoleId,
                    SystemStatusId = n.SystemStatusId,
                    UpdatedAt = n.UpdatedAt,
                    ClassId = n.ClassId,
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
