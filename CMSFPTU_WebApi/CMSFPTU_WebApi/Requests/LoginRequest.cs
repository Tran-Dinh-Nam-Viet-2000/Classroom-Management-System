﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Requests
{
    public class LoginRequest
    {
        public string AccountCode { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}