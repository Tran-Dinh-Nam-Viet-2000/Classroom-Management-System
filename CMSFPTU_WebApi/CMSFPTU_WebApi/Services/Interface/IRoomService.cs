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
        Task<ResponseApi> GetRoom(int id);
        Task<ResponseApi> Create(RoomRequest roomRequest);
        Task<ResponseApi> Update(int id, RoomRequest roomRequest);
        Task<ResponseApi> Delete(int? id);
        //Record deleted
        Task<IEnumerable<RoomResponse>> GetRecordDeleted();
        Task<ResponseApi> RoomDetails(int id);
        Task<ResponseApi> Restore(int? id);
    }
}
