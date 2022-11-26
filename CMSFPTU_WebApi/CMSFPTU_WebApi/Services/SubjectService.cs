using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
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
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).OrderByDescending(x => x.SubjectId).ToListAsync();

            return subjects;
        }

        public async Task<IEnumerable<SubjectResponse>> SearchSubject(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Subjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.SubjectCode.ToLower().Contains(keyword)
                                                                            || n.SubjectName.ToLower().Contains(keyword)
                                                                            || n.Description.ToLower().Contains(keyword)))
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<SubjectResponse>> SearchSubjectDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.Subjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.SubjectCode.ToLower().Contains(keyword)
                                                                            || n.SubjectName.ToLower().Contains(keyword)
                                                                            || n.Description.ToLower().Contains(keyword)))
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetSubject(int id)
        {
            var subject = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.SubjectId == id);
            if (subject == null || subject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.SubjectIsNull
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = subject
                };
            }
        }

        public async Task<ResponseApi> Create(SubjectRequest subjectRequest)
        {
            if ("".Equals(subjectRequest.SubjectCode) || "".Equals(subjectRequest.SubjectName))
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.Fail,
                };
            }
            var checkSubject = await _dbContext.Subjects.FirstOrDefaultAsync(n => n.SubjectCode == subjectRequest.SubjectCode);
            if (checkSubject != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.SubjectAlreadyExists
                };
            }
            var create = new Subject
            {
                SubjectCode = subjectRequest.SubjectCode,
                SubjectName = subjectRequest.SubjectName,
                SystemStatusId = (int)LkSystemStatus.Active
            };
            _dbContext.Add(create);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
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
                    Message = Messages.SubjectIsNull
                };
            }
            else
            {
                checkSubject.SubjectCode = subjectRequest.SubjectCode;
                checkSubject.SubjectName = subjectRequest.SubjectName;
                checkSubject.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();
            }
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
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
                    Message = Messages.SubjectIsNull
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
                Message = Messages.SuccessfullyDeleted,
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
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).OrderByDescending(x => x.SubjectId).ToListAsync();

            return subjects;
        }

        public async Task<ResponseApi> GetSubjectDeleted(int id)
        {
            var subject = await _dbContext.Subjects
                .Select(n => new SubjectResponse
                {
                    SubjectId = n.SubjectId,
                    SubjectCode = n.SubjectCode,
                    SubjectName = n.SubjectName,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.SubjectId == id);
            if (subject == null || subject.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.SubjectIsNull
                };
            }

            return new ResponseApi
            {
                Status = true,
                Message = Messages.DataIsNotNull,
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
                    Message = Messages.SubjectIsNull
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
                Message = Messages.SuccessfullyRestored
            };
        }
    }
}
