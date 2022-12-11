using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IRequestTeacherService
    {
        //Request from Teacher
        Task<IEnumerable<RequestTeacherResponse>> Get(int accountId);
        Task<IEnumerable<RequestTeacherResponse>> SearchTeacherRequest(string keyword, long accountId);
        Task<ResponseApi> GetRequestTeacher(int id);
        Task<ResponseApi> Create(RequestTeacherRequest teacherRequest);
        Task<ResponseApi> Delete(int id);

        //Admins take requests from teachers
        Task<IEnumerable<RequestTeacherResponse>> GetRequestFromTeacher();
        Task<IEnumerable<RequestTeacherResponse>> SearchRequestFromTeacher(string keyword);
        Task<ResponseApi> RequestApproval(int id);
        Task<ResponseApi> RequestReject(int id);

        //Filter
        Task<IEnumerable<RequestTypeResponse>> GetRequestType();
        Task<IEnumerable<ClassSubjectForRequestResponse>> GetSubject(int classId);
        Task<IEnumerable<RoomResponse>> GetRoom(int slotId, DateTime requestDate);
        Task<IEnumerable<FilterClassResponse>> GetClass(int accountId);
    }
}
