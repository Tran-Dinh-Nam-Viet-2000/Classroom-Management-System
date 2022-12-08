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
    public class ClassroomManagementController : ControllerBase
    {
        private readonly IClassroomManagementService _classroomManagementService;

        public ClassroomManagementController(IClassroomManagementService classroomManagementService)
        {
            _classroomManagementService = classroomManagementService;
        }

        [HttpGet]
        public async Task<IEnumerable<ClassroomResponse>> Get(DateTime date, bool status, int slotId)
        {
            var result = await _classroomManagementService.Get(date, status, slotId);
            return result;
        }
    }
}
