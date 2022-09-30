using CMSFPTU_WebApi.Entities;
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
        private readonly CMSFPTUContext _context;
        public ClassroomManagementController(CMSFPTUContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Class> GetAll()
        {
            return _context.Classes.ToList();
        }
    }
}
