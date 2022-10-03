using CMSFPTU_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Services.Interface
{
    public interface IStatusServices
    {
        List<StatusModels> GetAll();
    }
}
