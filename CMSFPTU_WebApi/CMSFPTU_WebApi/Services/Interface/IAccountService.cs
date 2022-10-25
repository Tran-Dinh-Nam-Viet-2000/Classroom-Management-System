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
        //Task<IEnumerable<AccountResponse>> GetAccountAutoComplete(string search);
        Task<IEnumerable<AccountResponse>> Get();
        Task<IEnumerable<AccountResponse>> Search(string keyword);
        //Task<IEnumerable<AccountSubjectResponse>> GetAccountByClassId(int id);
        Task<ResponseApi> GetAccount(int id);
        Task<ResponseApi> Create(AccountRequest accountRequest);
        Task<ResponseApi> Update(int id, UpdateAccountRequest updateAccountRequest);
        Task<ResponseApi> Delete(int id);
        Task<ResponseApi> Restore(int id);
        Task<IEnumerable<AccountResponse>> GetDeleted();
        Task<ResponseApi> GetAccountDeleted(int id);
    }
}
