using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Models;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponse>> Get();
        Task<ResponseApi> GetAccount(int id);
        Task<ResponseApi> Create(AccountRequest accountRequest);
        Task<ResponseApi> Update(int id, AccountRequest updateAccount);
        Task<ResponseApi> SoftDelete(int id);
        Task<ResponseApi> Restore(int id);
        //Function records deleted
        Task<IEnumerable<AccountResponse>> GetRecordDeleted();
        Task<ResponseApi> GetRecordDeletedById(int id);
    }
}
