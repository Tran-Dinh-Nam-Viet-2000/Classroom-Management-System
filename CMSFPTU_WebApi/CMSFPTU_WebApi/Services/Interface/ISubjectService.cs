using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectResponse>> Get();
        Task<IEnumerable<SubjectResponse>> SearchSubject(string keyword);
        Task<IEnumerable<SubjectResponse>> SearchSubjectDeleted(string keyword);
        Task<ResponseApi> GetSubject(int id);
        Task<ResponseApi> Create(SubjectRequest subjectRequest);
        Task<ResponseApi> Update(int id, SubjectRequest subjectRequest);
        Task<ResponseApi> Delete(int id);
        Task<IEnumerable<SubjectResponse>> GetDeleted();
        Task<ResponseApi> GetSubjectDeleted(int id);
        Task<ResponseApi> Restore(int id);
    }
}
