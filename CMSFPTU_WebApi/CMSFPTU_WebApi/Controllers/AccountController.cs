﻿using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IEnumerable<AccountResponse>> Get()
        {
            var result = await _accountService.Get();
            return result;
        }

        [HttpGet("get-account")]
        public async Task<ResponseApi> GetAccount(int id)
        {
            var result = await _accountService.GetAccount(id);
            return result;
        }

        [HttpPost("create")]
        public async Task<ResponseApi> Create([FromBody] AccountRequest accountRequest)
        {
            var result = await _accountService.Create(accountRequest);
            return result;
        }

        [HttpPut("update")]
        public async Task<ResponseApi> Update(int id, AccountRequest updateAccount)
        {
            var result = await _accountService.Update(id, updateAccount);
            return result;
        }
        [HttpDelete("soft-delete")]
        public async Task<ResponseApi> SoftDelete(int id)
        {
            var result = await _accountService.SoftDelete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _accountService.Restore(id);
            return result;
        }
        [HttpGet("is-deleted")]
        public async Task<IEnumerable<AccountResponse>> GetRecordDeleted()
        {
            var result = await _accountService.GetRecordDeleted();
            return result;
        }
        [HttpGet("get-record-deleted-by-id")]
        public async Task<ResponseApi> GetRecordDeletedById(int id)
        {
            var result = await _accountService.GetRecordDeletedById(id);
            return result;
        }
        [HttpDelete("hard-delete")]
        public async Task<ResponseApi> HardDelete(int id)
        {
            var result = await _accountService.HardDelete(id);
            return result;
        }
    }
}
