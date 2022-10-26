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
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }
        [HttpGet]
        public async Task<IEnumerable<ClassResponse>> Get()
        {
            var result = await _classService.Get();
            return result;
        }

        [HttpGet("search-class")]
        public async Task<IEnumerable<ClassResponse>> SearchClass(string keyword)
        {
            var result = await _classService.SearchClass(keyword);
            return result;
        }

        [HttpGet("search-class-deleted")]
        public async Task<IEnumerable<ClassResponse>> SearchClassDeleted(string keyword)
        {
            var result = await _classService.SearchClassDeleted(keyword);
            return result;
        }

        [HttpGet("get-class")]
        public async Task<ResponseApi> GetClass(int id)
        {
            var result = await _classService.GetClass(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] ClassRequest classRequest)
        {
            var result = await _classService.Create(classRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, ClassRequest classRequest)
        {
            var result = await _classService.Update(id, classRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _classService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _classService.Restore(id);
            return result;
        }
        [HttpGet("get-deleted")]
        public async Task<IEnumerable<ClassResponse>> GetDeleted()
        {
            var result = await _classService.GetDeleted();
            return result;
        }
        [HttpGet("get-class-deleted")]
        public async Task<ResponseApi> GetClassDeleted(int id)
        {
            var result = await _classService.GetClassDeleted(id);
            return result;
        }
    }
}
