using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IClassService
    {
        Task<IEnumerable<ClassResponse>> Get();
        //Task<IEnumerable<ClassResponse>> GetClassAutoComplete(string search);
        Task<ResponseApi> GetClass(int id);
        Task<ResponseApi> Create(ClassRequest classRequest);
        Task<ResponseApi> Update(int id, ClassRequest classRequest);
        Task<ResponseApi> Delete(int id);
        //Record deleted
        Task<IEnumerable<ClassResponse>> GetDeleted();
        Task<ResponseApi> GetClassDeleted(int id);
        Task<ResponseApi> Restore(int id);
    }
}
