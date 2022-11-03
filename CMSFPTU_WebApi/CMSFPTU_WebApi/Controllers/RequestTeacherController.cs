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
        private readonly IRequestTeacher _requestTeacher;

        public RequestTeacherController(IRequestTeacher requestTeacher)
        {
            _requestTeacher = requestTeacher;
        }

        //Request from Teacher
        [HttpGet]
        public async Task<IEnumerable<RequestTeacherResponse>> Get()
        {
            var result = await _requestTeacher.Get();
            return result;
        }

        //[HttpPost]
        //public async Task<ResponseApi> Create([FromForm] RequestTeacherRequest teacherRequest)
        //{
        //    var result = await _requestTeacher.Create(teacherRequest);
        //    return result;
        //}

        [HttpGet("search-teacher-request")]
        public async Task<IEnumerable<RequestTeacherResponse>> SearchTeacherRequest(string keyword)
        {
            var result = await _requestTeacher.SearchTeacherRequest(keyword);
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
    }
}
