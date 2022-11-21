using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class ClassSlotService : IClassSlotService
    {
        private readonly CMSFPTUContext _dbContext;

        public ClassSlotService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClassSlotResponse>> Get()
        {
            var classSlots = await _dbContext.ClassSlots
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return classSlots;
        }

        public async Task<ResponseApi> GetClassSlot(int id)
        {
            var classSlot = await _dbContext.ClassSlots
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassSlotId == id);
            if (classSlot == null || classSlot.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = classSlot
                };
            }
        }

        public async Task<IEnumerable<ClassSlotResponse>> SearchClassSlot(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassSlots
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Slot.StartTime.ToString().ToLower().Contains(keyword)
                                                                            || n.Slot.EndTime.ToString().ToLower().Contains(keyword)))
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<ClassSlotResponse>> SearchClassSlotDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassSlots
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Slot.StartTime.ToString().ToLower().Contains(keyword)
                                                                            || n.Slot.EndTime.ToString().ToLower().Contains(keyword)))
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> Create(ClassSlotRequest classSlotRequest)
        {
            var query = await _dbContext.ClassSlots.FirstOrDefaultAsync(n => n.ClassId == classSlotRequest.ClassId
                                                                          && n.SlotId == classSlotRequest.SlotId);
            if (query != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordAlreadyExists
                };
            }
            var classSlot = new ClassSlot
            {
                ClassId = classSlotRequest.ClassId,
                SlotId = classSlotRequest.SlotId,
                SystemStatusId = (int)LkSystemStatus.Active
            };

            _dbContext.ClassSlots.Add(classSlot);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
                Body = classSlot
            };
        }

        public async Task<ResponseApi> Update(int id, ClassSlotRequest classSlotRequest)
        {
            var classSlot = await _dbContext.ClassSlots.FirstOrDefaultAsync(n => n.ClassSlotId == id);

            if (classSlot == null || classSlot.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classSlot.SlotId = classSlotRequest.SlotId;
            classSlot.SystemStatusId = (int)LkSystemStatus.Active;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var classSlot = await _dbContext.ClassSlots.FirstOrDefaultAsync(n => n.ClassSlotId == id);
            if (classSlot == null || classSlot.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classSlot.SystemStatusId = (int)LkSystemStatus.Deleted;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<IEnumerable<ClassSlotResponse>> GetDeleted()
        {
            var classSlots = await _dbContext.ClassSlots
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return classSlots;
        }

        public async Task<ResponseApi> GetClassSlotDeleted(int id)
        {
            var classSlot = await _dbContext.ClassSlots
                .Select(n => new ClassSlotResponse
                {
                    ClassSlotId = n.ClassSlotId,
                    Class = n.Class,
                    Slot = n.Slot,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassSlotId == id);
            if (classSlot == null || classSlot.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = classSlot
                };
            }
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var classSlot = await _dbContext.ClassSlots.FirstOrDefaultAsync(n => n.ClassSlotId == id);
            if (classSlot == null || classSlot.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classSlot.SystemStatusId = (int)LkSystemStatus.Active;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        //Get list account by class
        public async Task<IEnumerable<AccountInClassResponse>> GetAccounts(int classId)
        {
            var accounts = await _dbContext.Accounts
                .Select(n => new AccountInClassResponse
                {
                    AccountId = n.AccountId,
                    AccountCode = n.AccountCode,
                    Email = n.Email,
                    Firstname = n.Firstname,
                    Lastname = n.Lastname,
                    Gender = n.Gender,
                    Phone = n.Phone,
                    SystemStatusId = n.SystemStatusId,
                    ClassId = (long)n.ClassId,
                    RoleId = n.RoleId
                }).Where(x => x.ClassId == classId && x.RoleId != (int)LkRoles.Teacher && x.RoleId != (int)LkRoles.Admin
                           && x.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return accounts;
        }
    }
}
