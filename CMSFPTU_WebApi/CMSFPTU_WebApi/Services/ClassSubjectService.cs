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
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly CMSFPTUContext _dbContext;

        public ClassSubjectService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClassSubjectResponse>> Get()
        {
            var classSubjects = await _dbContext.ClassSubjects
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    Subject = n.Subject,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    SystemStatusId = (int)n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).OrderByDescending(x => x.ClassSubjectId).ToListAsync();

            return classSubjects;
        }

        public async Task<IEnumerable<ClassSubjectResponse>> SearchClassSubject(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassSubjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Active && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    Subject = n.Subject,
                    SystemStatusId = (int)n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<IEnumerable<ClassSubjectResponse>> SearchClassSubjectDeleted(string keyword)
        {
            if ("".Equals(keyword))
            {
                return null;
            }
            var filter = await _dbContext.ClassSubjects
                .Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted && (n.Class.ClassCode.ToLower().Contains(keyword)
                                                                            || n.Subject.SubjectCode.ToLower().Contains(keyword)))
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    Subject = n.Subject,
                    SystemStatusId = (int)n.SystemStatusId
                }).ToListAsync();

            return filter;
        }

        public async Task<ResponseApi> GetClassSubject(int id)
        {
            var classSubject = await _dbContext.ClassSubjects
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    Subject = n.Subject,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    SystemStatusId = (int)n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassSubjectId == id);
            if (classSubject == null || classSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
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
                    Body = classSubject
                };
            }
        }

        public async Task<ResponseApi> Create(ClassSubjectRequest classSubjectRequest)
        {
            var query = await _dbContext.ClassSubjects.FirstOrDefaultAsync(n => n.ClassId == classSubjectRequest.ClassId 
                                                                             && n.SubjectId == classSubjectRequest.SubjectId);
            if (query != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordAlreadyExists,
                };
            }
            var classSubject = new ClassSubject
            {
                ClassId = classSubjectRequest.ClassId,
                SubjectId = classSubjectRequest.SubjectId,
                StartDate = classSubjectRequest.StartDate,
                EndDate = classSubjectRequest.EndDate,
                SystemStatusId = (int)LkSystemStatus.Active
            };

            _dbContext.ClassSubjects.AddRange(classSubject);
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };
        }

        public async Task<ResponseApi> Update(int id, ClassSubjectRequest classSubjectRequest)
        {
            var classSubject = await _dbContext.ClassSubjects.FirstOrDefaultAsync(n => n.ClassSubjectId == id);

            if (classSubject == null || classSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classSubject.SubjectId = classSubjectRequest.SubjectId;
            classSubject.StartDate = classSubjectRequest.StartDate;
            classSubject.EndDate = classSubjectRequest.EndDate;
            classSubject.SystemStatusId = (int)LkSystemStatus.Active;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyUpdated,
            };
        }

        public async Task<ResponseApi> Delete(int id)
        {
            var classSubject = await _dbContext.ClassSubjects.FirstOrDefaultAsync(n => n.ClassSubjectId == id);
            if (classSubject == null || classSubject.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            classSubject.SystemStatusId = (int)LkSystemStatus.Deleted;
            await _dbContext.SaveChangesAsync();

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var classSubject = await _dbContext.ClassSubjects.FirstOrDefaultAsync(n => n.ClassSubjectId == id);
            if (classSubject == null || classSubject.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.RecordIsNull,
                };
            }
            else
            {
                classSubject.SystemStatusId = (int)LkSystemStatus.Active;
                await _dbContext.SaveChangesAsync();

                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.SuccessfullyDeleted,
                };
            }
        }

        public async Task<IEnumerable<ClassSubjectResponse>> GetDeleted()
        {
            var classSubject = await _dbContext.ClassSubjects
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    Subject = n.Subject,
                    SystemStatusId = (int)n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).OrderByDescending(x => x.ClassSubjectId).ToListAsync();

            return classSubject;
        }

        public async Task<ResponseApi> GetClassSubjectDeleted(int id)
        {
            var classSubject = await _dbContext.ClassSubjects
                .Select(n => new ClassSubjectResponse
                {
                    ClassSubjectId = n.ClassSubjectId,
                    ClassId = n.ClassId,
                    ClassCode = n.Class.ClassCode,
                    Subject = n.Subject,
                    StartDate = n.StartDate,
                    EndDate = n.EndDate,
                    SystemStatusId = (int)n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassSubjectId == id);
            if (classSubject == null || classSubject.SystemStatusId == (int)LkSystemStatus.Active)
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
                    Body = classSubject
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
