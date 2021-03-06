﻿using System.Collections.Generic;
using PB.Dto;

namespace Photobook.Models
{
    public class ServerHost
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ServerHostResponse
    {
        public string name { get; set; }
        public string email { get; set; }
        public List<EventModel> events { get; set; }
    }
}