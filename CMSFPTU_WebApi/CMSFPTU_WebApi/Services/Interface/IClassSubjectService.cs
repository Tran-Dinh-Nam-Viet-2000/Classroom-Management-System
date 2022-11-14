using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IClassSubjectService
    {
        Task<IEnumerable<ClassSubjectResponse>> Get();
        Task<IEnumerable<ClassSubjectResponse>> SearchClassSubject(string keyword);
        Task<IEnumerable<ClassSubjectResponse>> SearchClassSubjectDeleted(string keyword);
        Task<ResponseApi> GetClassSubject(int id);
        Task<ResponseApi> Create(ClassSubjectRequest classSubjectRequest);
        Task<ResponseApi> Update(int id, ClassSubjectRequest classSubjectRequest);
        Task<ResponseApi> Delete(int id);
        Task<ResponseApi> Restore(int id);
        Task<IEnumerable<ClassSubjectResponse>> GetDeleted();
        Task<ResponseApi> GetClassSubjectDeleted(int id);
        //Get list account by class
        Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId);
    }
}
