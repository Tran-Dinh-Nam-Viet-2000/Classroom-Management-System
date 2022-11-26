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
    public class RoomTypeService : IRoomTypeService
    {
        private readonly CMSFPTUContext _dbContext;

        public RoomTypeService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<RoomTypeResponse>> Get()
        {
            var roomTypes = await _dbContext.RoomTypes
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    TypeName = n.TypeName,
                    Description = n.Description,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).OrderByDescending(x => x.TypeId).ToListAsync();

            return roomTypes;
        }

        public async Task<IEnumerable<RoomTypeResponse>> SearchRoomType(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.RoomTypes
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.TypeCode.ToLower().Contains(keyword)
                                                                            || n.TypeName.ToLower().Contains(keyword)))
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    TypeName = n.TypeName,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<RoomTypeResponse>> SearchRoomTypeDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.RoomTypes
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.TypeCode.ToLower().Contains(keyword)
                                                                            || n.TypeName.ToLower().Contains(keyword)))
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    TypeName = n.TypeName,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetRoomType(int id)
        {
            var roomType = await _dbContext.RoomTypes
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    Description = n.Description,
                    TypeName = n.TypeName,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.TypeId == id);
            if (roomType == null || roomType.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeIsNull
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = roomType
                };
            }
        }

        public async Task<ResponseApi> Create(RoomTypeRequest roomTypeRequest)
        {
            if ("".Equals(roomTypeRequest.TypeCode) || "".Equals(roomTypeRequest.TypeName))
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail,
                };
            }
            var checkRoomType = await _dbContext.RoomTypes.FirstOrDefaultAsync(n => n.TypeCode == roomTypeRequest.TypeCode 
                                                                                 || n.TypeName == roomTypeRequest.TypeName);
            if (checkRoomType != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeAlreadyExists,
                };
            }
            var createRoomType = new RoomType
            {
                TypeName = roomTypeRequest.TypeName,
                SystemStatusId = (int)LkSystemStatus.Active,
                TypeCode = roomTypeRequest.TypeCode,
                Description = roomTypeRequest.Description,
            };
            _dbContext.Add(createRoomType);
            await _dbContext.SaveChangesAsync();
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };
        }

        public async Task<ResponseApi> Update(int id, RoomTypeRequest roomTypeRequest)
        {
            var roomType = await _dbContext.RoomTypes.FirstOrDefaultAsync(n => n.TypeId == id);
            if (roomType == null || roomType.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeIsNull,
                };
            }
            else
            {
                roomType.TypeCode = roomTypeRequest.TypeCode;
                roomType.SystemStatusId = (int)LkSystemStatus.Active;
                roomType.TypeName = roomTypeRequest.TypeName;
                roomType.Description = roomTypeRequest.Description;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
                Body = roomType
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var roomType = await _dbContext.RoomTypes.FirstOrDefaultAsync(n => n.TypeId == id);
            if (roomType.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeIsNull,
                };
            }
            else
            {
                roomType.SystemStatusId = (int)LkSystemStatus.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<IEnumerable<RoomTypeResponse>> GetDeleted()
        {
            var rooms = await _dbContext.RoomTypes
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    TypeName = n.TypeName,
                    Description = n.Description,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).OrderByDescending(x => x.TypeId).ToListAsync();

            return rooms;
        }

        public async Task<ResponseApi> GetRoomTypeDeleted(int id)
        {
            var roomTypeDeleted = await _dbContext.RoomTypes
                .Select(n => new RoomTypeResponse
                {
                    TypeId = n.TypeId,
                    TypeCode = n.TypeCode,
                    TypeName = n.TypeName,
                    Description = n.Description,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.TypeId == id);
            if (roomTypeDeleted == null || roomTypeDeleted.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = roomTypeDeleted
                };
            }
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var room = await _dbContext.RoomTypes.FirstOrDefaultAsync(n => n.TypeId == id);
            if (room == null || room.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomTypeIsNull,
                };
            }
            else
            {
                room.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyRestored,
            };
        }
    }
}

