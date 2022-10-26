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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IEnumerable<SubjectResponse>> Get()
        {
            var result = await _subjectService.Get();
            return result;
        }

        [HttpGet("search-subject")]
        public async Task<IEnumerable<SubjectResponse>> SearchSubject(string keyword)
        {
            var result = await _subjectService.SearchSubject(keyword);
            return result;
        }

        [HttpGet("search-subject-deleted")]
        public async Task<IEnumerable<SubjectResponse>> SearchSubjectDeleted(string keyword)
        {
            var result = await _subjectService.SearchSubjectDeleted(keyword);
            return result;
        }

        [HttpGet("get-subject")]
        public async Task<ResponseApi> GetSubject(int id)
        {
            var result = await _subjectService.GetSubject(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create(SubjectRequest subjectRequest)
        {
            var result = await _subjectService.Create(subjectRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, SubjectRequest subjectRequest)
        {
            var result = await _subjectService.Update(id, subjectRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _subjectService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _subjectService.Restore(id);
            return result;
        }
        [HttpGet("get-deleted")]
        public async Task<IEnumerable<SubjectResponse>> GetDeleted()
        {
            var result = await _subjectService.GetDeleted();
            return result;
        }
        [HttpGet("get-subject-deleted")]
        public async Task<ResponseApi> GetSubjectDeleted(int id)
        {
            var result = await _subjectService.GetSubjectDeleted(id);
            return result;
        }
    }
}
