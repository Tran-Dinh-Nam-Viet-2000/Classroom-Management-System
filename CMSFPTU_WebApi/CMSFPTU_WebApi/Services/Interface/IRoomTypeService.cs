using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomTypeResponse>> Get();
        Task<IEnumerable<RoomTypeResponse>> SearchRoomType(string keyword);
        Task<IEnumerable<RoomTypeResponse>> SearchRoomTypeDeleted(string keyword);
        Task<ResponseApi> GetRoomType(int id);
        Task<ResponseApi> Create(RoomTypeRequest roomTypeRequest);
        Task<ResponseApi> Update(int id, RoomTypeRequest roomTypeRequest);
        Task<ResponseApi> Delete(int id);
        Task<IEnumerable<RoomTypeResponse>> GetDeleted();
        Task<ResponseApi> GetRoomTypeDeleted(int id);
        Task<ResponseApi> Restore(int id);
    }
}
