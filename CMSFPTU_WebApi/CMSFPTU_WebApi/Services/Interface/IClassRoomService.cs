using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IClassRoomService
    {
        Task<IEnumerable<ClassRoomResponse>> Get();
        Task<IEnumerable<ClassRoomResponse>> SearchClassRoom(string keyword);
        Task<IEnumerable<ClassRoomResponse>> SearchClassRoomDeleted(string keyword);
        Task<ResponseApi> GetClassRoom(int id);
        Task<ResponseApi> Create(ClassRoomRequest classSubjectRequest);
        Task<ResponseApi> Update(int id, ClassRoomRequest classSubjectRequest);
        Task<ResponseApi> Delete(int id);
        Task<ResponseApi> Restore(int id);
        Task<IEnumerable<ClassRoomResponse>> GetDeleted();
        Task<ResponseApi> GetClassRoomDeleted(int id);
        //Get list account by class
        Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId);
    }
}
