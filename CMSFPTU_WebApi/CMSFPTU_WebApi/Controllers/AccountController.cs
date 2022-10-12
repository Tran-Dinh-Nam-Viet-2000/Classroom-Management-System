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
        [HttpDelete("delete")]
        public async Task<ResponseApi> Delete(int id)
        {
            var result = await _accountService.Delete(id);
            return result;
        }
        [HttpPost("restore")]
        public async Task<ResponseApi> Restore(int id)
        {
            var result = await _accountService.Restore(id);
            return result;
        }
        [HttpGet("get-deleted")]
        public async Task<IEnumerable<AccountResponse>> GetDeleted()
        {
            var result = await _accountService.GetDeleted();
            return result;
        }
        [HttpGet("get-account-deleted")]
        public async Task<ResponseApi> GetAccountDeleted(int id)
        {
            var result = await _accountService.GetAccountDeleted(id);
            return result;
        }
    }
}
