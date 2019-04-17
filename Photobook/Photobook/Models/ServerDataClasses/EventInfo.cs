﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models.ServerDataClasses
{
    public class EventInfo
    {
        public string pin { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<GuestUser> guests { get; set; }
        public List<User> host { get; set; }
        public List<string> pictures { get; set; }
    }
}