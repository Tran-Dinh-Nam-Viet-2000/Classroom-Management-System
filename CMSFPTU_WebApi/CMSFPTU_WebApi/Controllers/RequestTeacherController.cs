using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestTeacherController : ControllerBase
    {
        private readonly IRequestTeacherService _requestTeacher;

        public RequestTeacherController(IRequestTeacherService requestTeacher)
        {
            _requestTeacher = requestTeacher;
        }

        //Request from Teacher
        [HttpGet("get-by-accountId")]
        public async Task<IEnumerable<RequestTeacherResponse>> Get(int accountId)
        {
            var result = await _requestTeacher.Get(accountId);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromForm] RequestTeacherRequest teacherRequest)
        {
            var result = await _requestTeacher.Create(teacherRequest);
            return result;
        }

        [HttpGet("search-teacher-request")]
        public async Task<IEnumerable<RequestTeacherResponse>> SearchTeacherRequest(string keyword, long accountId)
        {
            var result = await _requestTeacher.SearchTeacherRequest(keyword, accountId);
            return result;
        }

        [HttpGet("get-request-teacher")]
        public async Task<ResponseApi> GetRequestTeacher(int id)
        {
            var result = await _requestTeacher.GetRequestTeacher(id);
            return result;
        }

        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _requestTeacher.Delete(id);
            return result;
        }


        //Admins take requests from teachers

        [HttpGet("get-request-from-teacher")]
        public async Task<IEnumerable<RequestTeacherResponse>> GetRequestFromTeacher()
        {
            var result = await _requestTeacher.GetRequestFromTeacher();
            return result;
        }

        [HttpGet("search-request-from-teacher")]
        public async Task<IEnumerable<RequestTeacherResponse>> SearchRequestFromTeacher(string keyword)
        {
            var result = await _requestTeacher.SearchRequestFromTeacher(keyword);
            return result;
        }

        [HttpPost("request-approval")]
        public async Task<ResponseApi> RequestApproval(int id)
        {
            var result = await _requestTeacher.RequestApproval(id);
            return result;
        }

        [HttpPost("request-reject")]
        public async Task<ResponseApi> RequestReject(int id)
        {
            var result = await _requestTeacher.RequestReject(id);
            return result;
        }


        //Filter
        [HttpGet("get-request-type")]
        public async Task<IEnumerable<RequestTypeResponse>> GetRequestType()
        {
            var result = await _requestTeacher.GetRequestType();
            return result;
        }

        [HttpGet("get-subject")]
        public async Task<IEnumerable<ClassSubjectForRequestResponse>> GetSubject(int classId)
        {
            var result = await _requestTeacher.GetSubject(classId);
            return result;
        }

        [HttpGet("get-room")]
        public async Task<IEnumerable<RoomResponse>> GetRoom(int slotId, DateTime requestDate)
        {
            var result = await _requestTeacher.GetRoom(slotId, requestDate);
            return result;
        }
        [HttpGet("get-class")]
        public async Task<IEnumerable<FilterClassResponse>> GetClass(int accountId)
        {
            var result = await _requestTeacher.GetClass(accountId);
            return result;
        }
        
    }
}
