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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public async Task<IEnumerable<RoomResponse>> Get()
        {
            var result = await _roomService.Get();
            return result;
        }

        [HttpGet("get-room")]
        public async Task<ResponseApi> GetRoom(int id)
        {
            var result = await _roomService.GetRoom(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] RoomRequest roomRequest)
        {
            var result = await _roomService.Create(roomRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, RoomRequest roomRequest)
        {
            var result = await _roomService.Update(id, roomRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _roomService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _roomService.Restore(id);
            return result;
        }
        [HttpGet("get-record-deleted")]
        public async Task<IEnumerable<RoomResponse>> GetRecordDeleted()
        {
            var result = await _roomService.GetRecordDeleted();
            return result;
        }
        [HttpGet("get-record-deleted-by-id")]
        public async Task<ResponseApi> GetRecordDeletedById(int id)
        {
            var result = await _roomService.RoomDetails(id);
            return result;
        }
    }
}
