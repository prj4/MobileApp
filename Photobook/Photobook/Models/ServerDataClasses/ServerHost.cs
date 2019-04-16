using System;
using System.Collections.Generic;
using System.Text;
using Photobook.View;

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
        public List<Event> events { get; set; }
    }
}

