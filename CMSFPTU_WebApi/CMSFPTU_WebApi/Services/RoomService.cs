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
                    TypeId = n.TypeId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();
            return rooms;
        }

        public async Task<ResponseApi> GetRoom(int id)
        {
            var getRoom = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    TypeId = n.TypeId
                }).FirstOrDefaultAsync(n => n.RoomId == id);
            if (getRoom == null || getRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room does not exist"
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Success",
                    Body = getRoom
                };
            }
        }
        public async Task<ResponseApi> Create(RoomRequest roomRequest)
        {
            var checkRoomNumber = _dbContext.Rooms.FirstOrDefault(n => n.RoomNumber == roomRequest.RoomNumber);
            if (checkRoomNumber != null)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room is not correct"
                };
            }
            var createRoom = new Room
            {
                RoomNumber = roomRequest.RoomNumber,
                SystemStatusId = roomRequest.SystemStatusId,
                TypeId = roomRequest.TypeId
            };
            _dbContext.Add(createRoom);
            await _dbContext.SaveChangesAsync();
            return new ResponseApi
            {
                Status = true,
                Message = "Successfully created",
            };           
        }

        public async Task<ResponseApi> Update(int id, RoomRequest roomRequest)
        {
            var checkRoom = _dbContext.Rooms.FirstOrDefault(n => n.RoomId == id); 
            if (checkRoom == null || checkRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room does not exist"
                };
            } else
            {
                checkRoom.RoomNumber = roomRequest.RoomNumber;
                checkRoom.SystemStatusId = roomRequest.SystemStatusId;
                checkRoom.TypeId = roomRequest.TypeId;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = "Successfully updated",
                Body = checkRoom
            };
        }
        public async Task<ResponseApi> Delete(int? id)
        {
            var room = await _dbContext.Rooms.FirstOrDefaultAsync(n => n.RoomId == id);
            if (id == null || room.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room does not exist"
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
                Message = "Successfully delete",
            };
        }

        public async Task<IEnumerable<RoomResponse>> GetRecordDeleted()
        {
            var rooms = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    TypeId = n.TypeId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return rooms;
        }
        public async Task<ResponseApi> RoomDetails(int id)
        {
            var getRoom = await _dbContext.Rooms
                .Select(n => new RoomResponse
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId,
                    TypeId = n.TypeId
                }).FirstOrDefaultAsync(n => n.RoomId == id);
            if (getRoom == null || getRoom.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room does not exist"
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Success",
                    Body = getRoom
                };
            }
        }

        public async Task<ResponseApi> Restore(int? id)
        {
            var room = await _dbContext.Rooms.FirstOrDefaultAsync(n => n.RoomId == id);
            if (id == null || room.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = "Room does not exist"
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
                Message = "Successfully restore",
            };
        }
    }
}
