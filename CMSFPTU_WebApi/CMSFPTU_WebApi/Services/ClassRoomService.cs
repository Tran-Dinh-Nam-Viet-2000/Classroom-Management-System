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
    public class ClassRoomService : IClassRoomService
    {
        private readonly CMSFPTUContext _dbContext;

        public ClassRoomService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClassRoomResponse>> Get()
        {
            var classRooms = await _dbContext.ClassRooms
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return classRooms;
        }

        public async Task<IEnumerable<ClassRoomResponse>> SearchClassRoom(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassRooms
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Room.RoomNumber.ToString().ToLower().Contains(keyword)))
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<ClassRoomResponse>> SearchClassRoomDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassRooms
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Room.RoomNumber.ToString().ToLower().Contains(keyword)))
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetClassRoom(int id)
        {
            var classRoom = await _dbContext.ClassRooms
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassRoomId == id);
            if (classRoom == null || classRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
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
                    Body = classRoom
                };
            }
        }

        public async Task<ResponseApi> Create(ClassRoomRequest classSubjectRequest)
        {
            var query = await _dbContext.ClassRooms.FirstOrDefaultAsync(n => n.ClassId == classSubjectRequest.ClassId 
                                                                          && n.RoomId == classSubjectRequest.RoomId);
            if (query != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordAlreadyExists,
                };
            }
            var classRoom = new ClassRoom
            {
                ClassId = classSubjectRequest.ClassId,
                RoomId = classSubjectRequest.RoomId,
                SystemStatusId = (int)LkSystemStatus.Active
            };

            _dbContext.ClassRooms.AddRange(classRoom);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };
        }

        public async Task<ResponseApi> Update(int id, ClassRoomRequest classSubjectRequest)
        {
            var classRoom = await _dbContext.ClassRooms.FirstOrDefaultAsync(n => n.ClassRoomId == id);

            if (classRoom == null || classRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classRoom.RoomId = classSubjectRequest.RoomId;
            classRoom.SystemStatusId = (int)LkSystemStatus.Active;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var classRoom = await _dbContext.ClassRooms.FirstOrDefaultAsync(n => n.ClassRoomId == id);
            if (classRoom == null || classRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classRoom.SystemStatusId = (int)LkSystemStatus.Deleted;
            await _dbContext.SaveChangesAsync();
            
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var classRoom = await _dbContext.ClassRooms.FirstOrDefaultAsync(n => n.ClassRoomId == id);
            if (classRoom == null || classRoom.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                classRoom.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();

                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.SuccessfullyDeleted,
                };
            }
        }        

        public async Task<IEnumerable<ClassRoomResponse>> GetDeleted()
        {
            var classRooms = await _dbContext.ClassRooms
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return classRooms;
        }

        public async Task<ResponseApi> GetClassRoomDeleted(int id)
        {
            var classRoom = await _dbContext.ClassRooms
                .Select(n => new ClassRoomResponse
                {
                    ClassRoomId = n.ClassRoomId,
                    Class = n.Class,
                    Room = n.Room,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassRoomId == id);
            if (classRoom == null || classRoom.SystemStatusId == (int)LkSystemStatus.Active)
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
                    Body = classRoom
                };
            }
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
