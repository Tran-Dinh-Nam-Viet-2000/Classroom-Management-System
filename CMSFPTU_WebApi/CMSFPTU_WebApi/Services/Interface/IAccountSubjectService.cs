using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IAccountSubjectService
    {
        Task<IEnumerable<AccountSubjectResponse>> Get();
        Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubject(string keyword);
        Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubjectDeleted(string keyword);
        Task<ResponseApi> GetAccountSubject(int id);
        Task<ResponseApi> Create(AccountSubjectRequest accountSubjectRequest);
        Task<ResponseApi> Update(AccountSubjectRequest accountSubjectRequest);
        Task<ResponseApi> Delete(int id);
        Task<ResponseApi> Restore(int id);
        Task<IEnumerable<AccountSubjectResponse>> GetDeleted();
        Task<ResponseApi> GetAccountSubjectDeleted(int id);
    }
}
