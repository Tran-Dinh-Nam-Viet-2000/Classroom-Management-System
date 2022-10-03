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
    public class SystemStatusController : ControllerBase
    {
        private readonly IStatusServices _statusServices;

        public SystemStatusController(IStatusServices statusServices)
        {
            _statusServices = statusServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _statusServices.GetAll();
            return Ok(result);
        }
    }
}
