﻿using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly CMSFPTUContext _dbContext;

        public SubjectService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<SubjectResponse>> Get()
        {
            var subjects = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return subjects;
        }
        
        public async Task<ResponseApi> GetSubject(int id)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectId == id);
            if (checkSubject == null || checkSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject does not exist"
                };
            }
            var subject = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Success",
                Body = subject,
            };
        }

        public async Task<ResponseApi> Create(SubjectRequest subjectRequest)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectCode == subjectRequest.SubjectCode);
            if (checkSubject != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject already existed"
                };
            }
            var create = new Subject
            {
                SubjectCode = subjectRequest.SubjectCode,
                SubjectName = subjectRequest.SubjectName,
                SystemStatusId = subjectRequest.SystemStatusId
            };
            _dbContext.Add(create);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Successfully created",
                Body = create
            };
        }
        
        public async Task<ResponseApi> Update(int id, SubjectRequest subjectRequest)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectId == id);
            if (checkSubject == null || checkSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject does not exist"
                };
            }
            else
            {
                checkSubject.SubjectCode = subjectRequest.SubjectCode;
                checkSubject.SubjectName = subjectRequest.SubjectName;
                checkSubject.SystemStatusId = subjectRequest.SystemStatusId;
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseApi
            {
                Status = true,
                Message = "Successfully updated",
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectId == id);
            if (checkSubject == null || checkSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject does not exist"
                };
            }
            else
            {
                checkSubject.SystemStatusId = (int)LkSystemStatus.Deleted;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = "Successfully deleted",
            };
        }

        public async Task<IEnumerable<SubjectResponse>> GetDeleted()
        {
            var subjects = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return subjects;
        }

        public async Task<ResponseApi> GetSubjectDeleted(int id)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectId == id);
            if (checkSubject == null || checkSubject.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject does not exist"
                };
            }
            var subject = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return new ResponseApi
            {
                Status = true,
                Message = "Success",
                Body = subject,
            };
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectId == id);
            if (checkSubject == null || checkSubject.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = "Subject does not exist"
                };
            }
            else
            {
                checkSubject.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = "Successfully deleted",
            };
        }
    }
}