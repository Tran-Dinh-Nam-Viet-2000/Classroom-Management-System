using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services;
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
    public class RoomTypeController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        [HttpGet]
        public async Task<IEnumerable<RoomTypeResponse>> Get()
        {
            var result = await _roomTypeService.Get();
            return result;
        }

        [HttpGet("search-room-type")]
        public async Task<IEnumerable<RoomTypeResponse>> SearchRoomType(string keyword)
        {
            var result = await _roomTypeService.SearchRoomType(keyword);
            return result;
        }

        [HttpGet("search-room-type-deleted")]
        public async Task<IEnumerable<RoomTypeResponse>> SearchRoomTypeDeleted(string keyword)
        {
            var result = await _roomTypeService.SearchRoomTypeDeleted(keyword);
            return result;
        }

        [HttpGet("get-room-type")]
        public async Task<ResponseApi> GetRoomType(int id)
        {
            var result = await _roomTypeService.GetRoomType(id);
            return result;
        }
        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] RoomTypeRequest roomTypeRequest)
        {
            var result = await _roomTypeService.Create(roomTypeRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, RoomTypeRequest roomTypeRequest)
        {
            var result = await _roomTypeService.Update(id, roomTypeRequest);
            return result;
        }
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _roomTypeService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _roomTypeService.Restore(id);
            return result;
        }
        [HttpGet("get-deleted")]
        public async Task<IEnumerable<RoomTypeResponse>> GetDeleted()
        {
            var result = await _roomTypeService.GetDeleted();
            return result;
        }
        [HttpGet("get-room-type-deleted")]
        public async Task<ResponseApi> GetRoomTypeDeleted(int id)
        {
            var result = await _roomTypeService.GetRoomTypeDeleted(id);
            return result;
        }
    }
}
