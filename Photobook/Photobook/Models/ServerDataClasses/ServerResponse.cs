using Photobook.Models.ServerDataClasses;

namespace Photobook.Models
{
    public class ServerEvent
    {
        public string name { get; set; }
        public EventInfo Event { get; set; }
    }
}