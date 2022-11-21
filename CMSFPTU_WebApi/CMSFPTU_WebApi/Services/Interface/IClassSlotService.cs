using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IClassSlotService
    {
        Task<IEnumerable<ClassSlotResponse>> Get();
        Task<IEnumerable<ClassSlotResponse>> SearchClassSlot(string keyword);
        Task<IEnumerable<ClassSlotResponse>> SearchClassSlotDeleted(string keyword);
        Task<ResponseApi> GetClassSlot(int id);
        Task<ResponseApi> Create(ClassSlotRequest classSlotRequest);
        Task<ResponseApi> Update(int id, ClassSlotRequest classSlotRequest);
        Task<ResponseApi> Delete(int id);
        Task<ResponseApi> Restore(int id);
        Task<IEnumerable<ClassSlotResponse>> GetDeleted();
        Task<ResponseApi> GetClassSlotDeleted(int id);
        //Get list account by class
        Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId);
    }
}
