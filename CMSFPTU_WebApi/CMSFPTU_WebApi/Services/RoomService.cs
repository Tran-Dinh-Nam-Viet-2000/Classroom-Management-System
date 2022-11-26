using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly CMSFPTUContext _dbContext;

        public RoomService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<RoomResponse>> Get()
        {
            var rooms = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    Type = n.Type
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).OrderByDescending(x => x.RoomId).ToListAsync();
            return rooms;
        }

        public async Task<IEnumerable<RoomResponse>> SearchRoom(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Rooms
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.RoomNumber.ToString().Contains(keyword)
                                                                            || n.Type.TypeCode.ToLower().Contains(keyword)
                                                                            || n.Type.TypeName.ToLower().Contains(keyword)))
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    Type = n.Type,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<RoomResponse>> SearchRoomDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Rooms
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.RoomNumber.ToString().Contains(keyword)
                                                                            || n.Type.TypeCode.ToLower().Contains(keyword)
                                                                            || n.Type.TypeName.ToLower().Contains(keyword)))
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    Type = n.Type,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetRoom(int id)
        {
            var room = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    Type = n.Type
                }).FirstOrDefaultAsync(n => n.RoomId == id);
            if (room == null || room.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = room
                };
            }
        }
        public async Task<ResponseApi> Create(RoomRequest roomRequest)
        {
            if ("".Equals(roomRequest.RoomNumber) || "".Equals(roomRequest.TypeId))
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail,
                };
            }
            var checkRoomNumber = _dbContext.Rooms.FirstOrDefault(n => n.RoomNumber == roomRequest.RoomNumber);
            if (checkRoomNumber != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomAlreadyExists,
                };
            }
            var createRoom = new Room
            {
                RoomNumber = roomRequest.RoomNumber,
                SystemStatusId = (int)LkSystemStatus.Active,
                TypeId = roomRequest.TypeId
            };
            _dbContext.Add(createRoom);
            await _dbContext.SaveChangesAsync();
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };           
        }

        public async Task<ResponseApi> Update(int id, RoomRequest roomRequest)
        {
            var checkRoom = _dbContext.Rooms.FirstOrDefault(n => n.RoomId == id);
            if (checkRoom == null || checkRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomIsNull,
                };
            } else
            {
                checkRoom.RoomNumber = roomRequest.RoomNumber;
                checkRoom.SystemStatusId = (int)LkSystemStatus.Active;
                checkRoom.TypeId = roomRequest.TypeId;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
                Body = checkRoom
            };
        }
        public async Task<ResponseApi> Delete(int id)
        {
            var room = await _dbContext.Rooms.FirstOrDefaultAsync(n => n.RoomId == id);
            if (room.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomIsNull,
                };
            }
            else
            {
                room.SystemStatusId = (int)LkSystemStatus.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<IEnumerable<RoomResponse>> GetDeleted()
        {
            var rooms = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    Type = n.Type
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).OrderByDescending(x => x.RoomId).ToListAsync();

            return rooms;
        }
        public async Task<ResponseApi> GetRoomDeleted(int id)
        {
            var getRoom = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    Type = n.Type
                }).FirstOrDefaultAsync(n => n.RoomId == id);
            if (getRoom == null || getRoom.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = getRoom
                };
            }
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var room = await _dbContext.Rooms.FirstOrDefaultAsync(n => n.RoomId == id);
            if (room == null || room.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RoomIsNull,
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
