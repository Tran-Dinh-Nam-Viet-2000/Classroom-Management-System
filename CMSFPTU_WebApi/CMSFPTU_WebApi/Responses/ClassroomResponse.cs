﻿using CMSFPTU_WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSFPTU_WebApi.Responses
{
    public class ClassroomResponse
    {
        public DateTime Date { get; set; }
        public int RoomNumber { get; set; }
        public long SlotId { get; set; }
    }
}
