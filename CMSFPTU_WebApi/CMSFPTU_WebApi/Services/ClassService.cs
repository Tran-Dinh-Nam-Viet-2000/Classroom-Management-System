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
    public class ClassService : IClassService
    {
        private readonly CMSFPTUContext _dbContext;

        public ClassService(CMSFPTUContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClassResponse>> Get()
        {
            var classes = await _dbContext.Classes
                .Select(n => new ClassResponse
                {
                    ClassId = n.ClassId,
                    ClassCode = n.ClassCode,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Active).ToListAsync();

            return classes;
        }

        //public async Task<IEnumerable<ClassResponse>> GetClassAutoComplete(string search)
        //{
        //    var query = _dbContext.Set<Class>().Where(r => r.SystemStatusId == (int)LkSystemStatus.Active);
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        var filters = search.Split(' ');
        //        foreach (var kw in filters)
        //        {
        //            query = query.Where(n => n.ClassCode.ToLower().Contains(kw));
        //        }
        //    }
        //    return (await query.Select(n => new ClassResponse
        //    {
        //        ClassId = n.ClassId,
        //        ClassCode = n.ClassCode,
        //        SystemStatusId = n.SystemStatusId
        //    }).ToListAsync());
    
        public async Task<ResponseApi> GetClass(int id)
        {
            var getClass = await _dbContext.Classes
                .Select(n => new Class
                {
                    ClassId = n.ClassId,
                    ClassCode = n.ClassCode,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassId == id);
            if (getClass == null || getClass.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = getClass
                };
            }
        }

        public async Task<ResponseApi> Create(ClassRequest roomRequest)
        {
            var checkClass = await _dbContext.Classes.FirstOrDefaultAsync(n => n.ClassCode == roomRequest.ClassCode);
            if (checkClass != null)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassAlreadyExists,
                };
            }
            var statusIsActive = (int)LkSystemStatus.Active;
            var createClass = new Class
            {
                ClassCode = roomRequest.ClassCode,
                SystemStatusId = statusIsActive
            };
            _dbContext.Add(createClass);
            await _dbContext.SaveChangesAsync();
            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyAddedNew,
            };
        }

        public async Task<ResponseApi> Update(int id, ClassRequest roomRequest)
        {
            var checkClass = await _dbContext.Classes.FirstOrDefaultAsync(n => n.ClassId == id);
            var statusIsActive = (int)LkSystemStatus.Active;
            if (checkClass == null || checkClass.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassIsNull,
                };
            }
            else
            {
                checkClass.ClassCode = roomRequest.ClassCode;
                checkClass.SystemStatusId = statusIsActive;
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
            var checkClass = await _dbContext.Classes.FirstOrDefaultAsync(n => n.ClassId == id);
            if (checkClass == null || checkClass.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassIsNull,
                };
            }
            else
            {
                checkClass.SystemStatusId = (int)LkSystemStatus.Deleted;
                await _dbContext.SaveChangesAsync();
            }

            return new ResponseApi
            {
                Status = true,
                Message = Messages.SuccessfullyDeleted,
            };
        }

        public async Task<IEnumerable<ClassResponse>> GetDeleted()
        {
            var classes = await _dbContext.Classes
                .Select(n => new ClassResponse
                {
                    ClassId = n.ClassId,
                    ClassCode = n.ClassCode,
                    SystemStatusId = n.SystemStatusId
                }).Where(n => n.SystemStatusId == (int)LkSystemStatus.Deleted).ToListAsync();

            return classes;
        }

        public async Task<ResponseApi> GetClassDeleted(int id)
        {
            var getClass = await _dbContext.Classes
                .Select(n => new Class
                {
                    ClassId = n.ClassId,
                    ClassCode = n.ClassCode,
                    SystemStatusId = n.SystemStatusId
                }).FirstOrDefaultAsync(n => n.ClassId == id);
            if (getClass == null || getClass.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassIsNull,
                };
            }
            else
            {
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.DataIsNotNull,
                    Body = getClass
                };
            }
        }

        public async Task<ResponseApi> Restore(int id)
        {
            var checkClass = await _dbContext.Classes.FirstOrDefaultAsync(n => n.ClassId == id);
            if (checkClass == null || checkClass.SystemStatusId == (int)LkSystemStatus.Active)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.ClassIsNull,
                };
            }
            else
            {
                checkClass.SystemStatusId = (int)LkSystemStatus.Active;
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
