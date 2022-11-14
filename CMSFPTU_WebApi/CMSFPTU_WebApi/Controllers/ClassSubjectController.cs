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
    public class ClassSubjectController : ControllerBase
    {
        private IClassSubjectService _classSubjectService;

        public ClassSubjectController(IClassSubjectService classSubjectService)
        {
            _classSubjectService = classSubjectService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassSubjectResponse>> Get()
        {
            var result = await _classSubjectService.Get();
            return result;
        }

        [HttpGet("search-class-subject")]
        public async Task<IEnumerable<ClassSubjectResponse>> SearchClassSubject(string keyword)
        {
            var result = await _classSubjectService.SearchClassSubject(keyword);
            return result;
        }

        [HttpGet("search-class-subject-deleted")]
        public async Task<IEnumerable<ClassSubjectResponse>> SearchClassSubjectDeleted(string keyword)
        {
            var result = await _classSubjectService.SearchClassSubjectDeleted(keyword);
            return result;
        }

        [HttpGet("get-class-subject")]
        public async Task<ResponseApi> GetClassSubject(int id)
        {
            var result = await _classSubjectService.GetClassSubject(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] ClassSubjectRequest classSubjectRequest)
        {
            var result = await _classSubjectService.Create(classSubjectRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, ClassSubjectRequest classSubjectRequest)
        {
            var result = await _classSubjectService.Update(id, classSubjectRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _classSubjectService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _classSubjectService.Restore(id);
            return result;
        }

        [HttpGet("get-deleted")]
        public async Task<IEnumerable<ClassSubjectResponse>> GetDeleted()
        {
            var result = await _classSubjectService.GetDeleted();
            return result;
        }
        [HttpGet("get-class-subject-deleted")]
        public async Task<ResponseApi> GetClassSubjectDeleted(int id)
        {
            var result = await _classSubjectService.GetClassSubjectDeleted(id);
            return result;
        }

        //Get list account by class
        [HttpGet("get-accounts")]
        public async Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId)
        {
            var result = await _classSubjectService.GetAccounts(classId);
            return result;
        }
    }
}
