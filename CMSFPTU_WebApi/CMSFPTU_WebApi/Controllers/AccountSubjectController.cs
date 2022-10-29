using CMSFPTU_WebApi.Entities;
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
    public class AccountSubjectController : ControllerBase
    {
        private IAccountSubjectService _accountSubjectService;

        public AccountSubjectController(IAccountSubjectService accountSubjectService)
        {
            _accountSubjectService = accountSubjectService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountSubjectResponse>> Get()
        {
            var result = await _accountSubjectService.Get();
            return result;
        }

        [HttpGet("search-account-subject")]
        public async Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubject(string keyword)
        {
            var result = await _accountSubjectService.SearchAccountSubject(keyword);
            return result;
        }

        [HttpGet("search-account-subject-deleted")]
        public async Task<IEnumerable<AccountSubjectResponse>> SearchAccountSubjectDeleted(string keyword)
        {
            var result = await _accountSubjectService.SearchAccountSubjectDeleted(keyword);
            return result;
        }

        [HttpGet("get-account-subject")]
        public async Task<ResponseApi> GetAccountSubject(int id)
        {
            var result = await _accountSubjectService.GetAccountSubject(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] AccountSubjectRequest accountSubjectRequest)
        {
            var result = await _accountSubjectService.Create(accountSubjectRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(AccountSubjectRequest accountSubjectRequest)
        {
            var result = await _accountSubjectService.Update(accountSubjectRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _accountSubjectService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _accountSubjectService.Restore(id);
            return result;
        }

        [HttpGet("get-deleted")]
        public async Task<IEnumerable<AccountSubjectResponse>> GetDeleted()
        {
            var result = await _accountSubjectService.GetDeleted();
            return result;
        }
        [HttpGet("get-account-subject-deleted")]
        public async Task<ResponseApi> GetAccountSubjectDeleted(int id)
        {
            var result = await _accountSubjectService.GetAccountSubjectDeleted(id);
            return result;
        }
    }
}
