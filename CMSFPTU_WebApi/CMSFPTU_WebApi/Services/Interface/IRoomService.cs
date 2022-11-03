using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponse>> Get();
        Task<IEnumerable<RoomResponse>> SearchRoom(string keyword);
        Task<IEnumerable<RoomResponse>> SearchRoomDeleted(string keyword);
        Task<ResponseApi> GetRoom(int id);
        Task<ResponseApi> Create(RoomRequest roomRequest);
        Task<ResponseApi> Update(int id, RoomRequest roomRequest);
        Task<ResponseApi> Delete(int id);
        Task<IEnumerable<RoomResponse>> GetDeleted();
        Task<ResponseApi> GetRoomDeleted(int id);
        Task<ResponseApi> Restore(int id);
    }
}
