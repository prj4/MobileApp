using System;
using System.Collections.Generic;
using System.Text;
using Photobook.Models.ServerDataClasses;
using Photobook.View;

namespace Photobook.Models
{
    public class ServerResponse
    {
        public string name { get; set; }
        public EventInfo Event { get; set; }
    }
}
