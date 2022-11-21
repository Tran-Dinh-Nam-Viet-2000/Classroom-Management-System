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
    public class ClassRoomController : ControllerBase
    {
        private IClassRoomService _classRoomService;

        public ClassRoomController(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassRoomResponse>> Get()
        {
            var result = await _classRoomService.Get();
            return result;
        }

        [HttpGet("search-class-room")]
        public async Task<IEnumerable<ClassRoomResponse>> SearchClassRoom(string keyword)
        {
            var result = await _classRoomService.SearchClassRoom(keyword);
            return result;
        }

        [HttpGet("search-class-room-deleted")]
        public async Task<IEnumerable<ClassRoomResponse>> SearchClassRoomDeleted(string keyword)
        {
            var result = await _classRoomService.SearchClassRoomDeleted(keyword);
            return result;
        }

        [HttpGet("get-class-room")]
        public async Task<ResponseApi> GetClassRoom(int id)
        {
            var result = await _classRoomService.GetClassRoom(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] ClassRoomRequest classRoomRequest)
        {
            var result = await _classRoomService.Create(classRoomRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, ClassRoomRequest classRoomRequest)
        {
            var result = await _classRoomService.Update(id, classRoomRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _classRoomService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _classRoomService.Restore(id);
            return result;
        }

        [HttpGet("get-deleted")]
        public async Task<IEnumerable<ClassRoomResponse>> GetDeleted()
        {
            var result = await _classRoomService.GetDeleted();
            return result;
        }
        [HttpGet("get-class-room-deleted")]
        public async Task<ResponseApi> GetClassRoomDeleted(int id)
        {
            var result = await _classRoomService.GetClassRoomDeleted(id);
            return result;
        }

        //Get list account by class
        [HttpGet("get-accounts")]
        public async Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId)
        {
            var result = await _classRoomService.GetAccounts(classId);
            return result;
        }
    }
}
