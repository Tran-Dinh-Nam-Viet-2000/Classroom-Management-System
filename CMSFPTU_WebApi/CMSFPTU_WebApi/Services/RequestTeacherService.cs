﻿using CMSFPTU_WebApi.Constants;
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
    public class RequestTeacherService : IRequestTeacherService
    {
        private readonly CMSFPTUContext _dbContext;

        public RequestTeacherService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RequestTeacherResponse>> Get()
        {
            var request = await _dbContext.Requests
                .Select(n => new RequestTeacherResponse
                {
                    RequestId = n.RequestId,
                    RequestType = n.RequestType,
                    Class = n.Class,
                    Room = n.Room,
                    Slot = n.Slot,
                    Subject = n.Subject,
                    RequestBy = n.RequestBy,
                    RequestDate = n.RequestDate,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.WaitingForApproval
                           || n.SystemStatusId == (int)LkSystemStatus.Approved
                           || n.SystemStatusId == (int)LkSystemStatus.Rejected).ToListAsync();

            return request;
        }

        public async Task<ResponseApi> GetRequestTeacher(int id)
        {
            var request = await _dbContext.Requests
                .Select(n => new Request
                {
                    RequestId = n.RequestId,
                    RequestName = n.RequestName,
                    Class = n.Class,
                    Room = n.Room,
                    Slot = n.Slot,
                    Subject = n.Subject,
                    RequestBy = n.RequestBy,
                    RequestDate = n.RequestDate,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.RequestId == id || n.SystemStatusId == (int)LkSystemStatus.WaitingForApproval
                                                              || n.SystemStatusId == (int)LkSystemStatus.Approved
                                                              || n.SystemStatusId == (int)LkSystemStatus.Rejected);

            if (request == null || request.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.RecordIsNull,
                };
            }

            return new ResponseApi
            {
                Status = true,
                Message = Messages.DataIsNotNull,
                Body = request
            };
        }

        public async Task<IEnumerable<RequestTeacherResponse>> SearchTeacherRequest(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }

            var filter = await _dbContext.Requests.Where(n => n.SystemStatusId == (int)LkSystemStatus.WaitingForApproval
                                                           && n.SystemStatusId == (int)LkSystemStatus.Approved
                                                           && n.SystemStatusId == (int)LkSystemStatus.Rejected
                                                           && (n.Class.ClassCode.ToLower().Contains(keyword) || n.RequestType.RequestName.ToLower().Contains(keyword)
                                                                                                             || n.Room.RoomNumber.ToString().ToLower().Contains(keyword)
                                                                                                             || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new RequestTeacherResponse
                {
                    RequestId = n.RequestId,
                    RequestType = n.RequestType,
                    Class = n.Class,
                    Room = n.Room,
                    Slot = n.Slot,
                    Subject = n.Subject,
                    RequestBy = n.RequestBy,
                    RequestDate = n.RequestDate,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<RequestTeacherResponse>> SearchRequestFromTeacher(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }

            var filter = await _dbContext.Requests.Where(n => n.SystemStatusId == (int)LkSystemStatus.WaitingForApproval
                                                          && (n.Class.ClassCode.ToLower().Contains(keyword) || n.RequestType.RequestName.ToLower().Contains(keyword)
                                                                                                            || n.Room.RoomNumber.ToString().ToLower().Contains(keyword)
                                                                                                            || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new RequestTeacherResponse
                {
                    RequestId = n.RequestId,
                    RequestType = n.RequestType,
                    Class = n.Class,
                    Room = n.Room,
                    Slot = n.Slot,
                    Subject = n.Subject,
                    RequestBy = n.RequestBy,
                    RequestDate = n.RequestDate,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> Create(RequestTeacherRequest teacherRequest)
        {
            var createRequest = new Request
            {
                RequestTypeId = teacherRequest.RequestTypeId,
                ClassId = teacherRequest.ClassId,
                RoomId = teacherRequest.RoomId,
                SlotId = teacherRequest.SlotId,
                SubjectId = teacherRequest.SubjectId,
                RequestBy = teacherRequest.AccountId,
                SystemStatusId = (int)LkSystemStatus.WaitingForApproval,
                RequestDate = teacherRequest.RequestDate
            };

            if (teacherRequest.RequestDate < DateTime.Now)
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.Fail
                };
            }
            _dbContext.Add(createRequest);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var request = _dbContext.Requests.FirstOrDefault(n => n.RequestId == id);
            if (request == null || request.SystemStatusId == (int)LkSystemStatus.Approved || request.SystemStatusId == (int)LkSystemStatus.Rejected
                || request.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            request.SystemStatusId = (int)LkSystemStatus.Deleted;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }


        //Admins take requests from teachers
        public async Task<IEnumerable<RequestTeacherResponse>> GetRequestFromTeacher()
        {
            var request = await _dbContext.Requests
                .Select(n => new RequestTeacherResponse
                {
                    RequestId = n.RequestId,
                    RequestType = n.RequestType,
                    Class = n.Class,
                    Room = n.Room,
                    Slot = n.Slot,
                    Subject = n.Subject,
                    RequestBy = n.RequestBy,
                    RequestDate = n.RequestDate,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.WaitingForApproval).ToListAsync();

            return request;
        }

        public async Task<ResponseApi> RequestApproval(int id)
        {
            var request = _dbContext.Requests.FirstOrDefault(n => n.RequestId == id);
            if (request == null || request.SystemStatusId == (int)LkSystemStatus.Approved || request.SystemStatusId == (int)LkSystemStatus.Rejected
                || request.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            request.SystemStatusId = (int)LkSystemStatus.Approved;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyApproved,
            };
        }

        public async Task<ResponseApi> RequestReject(int id)
        {
            var request = _dbContext.Requests.FirstOrDefault(n => n.RequestId == id);
            if (request == null || request.SystemStatusId == (int)LkSystemStatus.Approved || request.SystemStatusId == (int)LkSystemStatus.Rejected
                || request.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            request.SystemStatusId = (int)LkSystemStatus.Rejected;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyRejected,
            };
        }


        //Filter
        public async Task<IEnumerable<RequestTypeResponse>> GetRequestType()
        {
            var requestTypes =  await _dbContext.RequestTypes
                .Select(n => new RequestTypeResponse
                {
                    RequestTypeId = n.RequestTypeId,
                    RequestName = n.RequestName
                }).ToListAsync();

            return requestTypes;
        }

        public async Task<IEnumerable<ClassSubjectForRequestResponse>> GetSubject(int classId)
        {
            var classes = await _dbContext.ClassSubjects
                .Select(n => new ClassSubjectForRequestResponse
                {
                    ClassId = n.ClassId,
                    ClassSubjectId = n.ClassSubjectId,
                    //Subject = n.Subject,
                    SubjectId = n.SubjectId,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && n.ClassId == classId).ToListAsync();

            return classes;
        }

        public async Task<IEnumerable<RoomResponse>> GetRoom(int slotId, DateTime requestDate)
        {
            var requests = await _dbContext.Requests.Where(x => x.SlotId == slotId && x.RequestDate == requestDate)
                .Select(x => x.Room.RoomId).ToListAsync();
            var rooms = await _dbContext.Rooms.Where(x => !requests.Contains(x.RoomId))
                .Select(n => new RoomResponse 
                {
                    RoomId = n.RoomId,
                    RoomNumber = n.RoomNumber,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return rooms;
        }
    }
}