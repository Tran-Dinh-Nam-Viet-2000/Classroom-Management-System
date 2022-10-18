using CMSFPTU_WebApi.Constants;
using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Enums;
using CMSFPTU_WebApi.Models;
using CMSFPTU_WebApi.Requests;
using CMSFPTU_WebApi.Responses;
using CMSFPTU_WebApi.Services.Interface;
using CMSFPTU_WebApi.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly CMSFPTUContext _dbContext;

        public LoginService(IConfiguration configuration,CMSFPTUContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<ResponseApi> Login(LoginRequest loginRequest)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountCode == loginRequest.AccountCode
                                                                          && x.PasswordHash == Md5.MD5Hash(loginRequest.Password));
            if (account == null || account.SystemStatusId == (int)LkSystemStatus.Deleted)
            {
                return new ResponseApi
                {
                    Status = false,
                    Message = Messages.AccountIsNull,
                };
            }
            else
            {
                string token = GenerateToken(account);
                var body = new
                {
                    token = token
                };
                return new ResponseApi
                {
                    Status = true,
                    Message = Messages.SuccessfullyLogined,
                    Body = body
                };
            }
        }

        //Setting token
        private string GenerateToken(Account accountInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("accountId", accountInfo.AccountId.ToString()),
                new Claim("lastName", accountInfo.Lastname),
                new Claim("email", accountInfo.Email),
                new Claim("roleId", accountInfo.RoleId.ToString()),
            };
            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
              _configuration["Tokens:Issuer"],
              claims,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
