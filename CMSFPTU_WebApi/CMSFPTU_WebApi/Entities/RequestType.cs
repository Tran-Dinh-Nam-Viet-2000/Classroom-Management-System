using System;
using System.Collections.Generic;

#nullable disable

namespace CMSFPTU_WebApi.Entities
{
    public partial class RequestType
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }

        public long RequestTypeId { get; set; }
        public string RequestName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
