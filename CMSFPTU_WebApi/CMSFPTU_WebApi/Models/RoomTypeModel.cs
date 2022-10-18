using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Models
{
    public class RoomTypeModel
    {
        public long TypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public int? SystemStatusId { get; set; }
    }
}
