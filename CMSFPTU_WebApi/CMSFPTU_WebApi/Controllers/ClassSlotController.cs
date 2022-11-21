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
    public class ClassSlotController : ControllerBase
    {
        private IClassSlotService _classSlotService;

        public ClassSlotController(IClassSlotService classSlotService)
        {
            _classSlotService = classSlotService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassSlotResponse>> Get()
        {
            var result = await _classSlotService.Get();
            return result;
        }

        [HttpGet("search-class-slot")]
        public async Task<IEnumerable<ClassSlotResponse>> SearchClassSlot(string keyword)
        {
            var result = await _classSlotService.SearchClassSlot(keyword);
            return result;
        }

        [HttpGet("search-class-slot-deleted")]
        public async Task<IEnumerable<ClassSlotResponse>> SearchClassSlotDeleted(string keyword)
        {
            var result = await _classSlotService.SearchClassSlotDeleted(keyword);
            return result;
        }

        [HttpGet("get-class-slot")]
        public async Task<ResponseApi> GetClassSlot(int id)
        {
            var result = await _classSlotService.GetClassSlot(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] ClassSlotRequest classSlotRequest)
        {
            var result = await _classSlotService.Create(classSlotRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, ClassSlotRequest classSlotRequest)
        {
            var result = await _classSlotService.Update(id, classSlotRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _classSlotService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _classSlotService.Restore(id);
            return result;
        }

        [HttpGet("get-deleted")]
        public async Task<IEnumerable<ClassSlotResponse>> GetDeleted()
        {
            var result = await _classSlotService.GetDeleted();
            return result;
        }
        [HttpGet("get-class-slot-deleted")]
        public async Task<ResponseApi> GetClassSlotDeleted(int id)
        {
            var result = await _classSlotService.GetClassSlotDeleted(id);
            return result;
        }

        //Get list account by class
        [HttpGet("get-accounts")]
        public async Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId)
        {
            var result = await _classSlotService.GetAccounts(classId);
            return result;
        }
    }
}
