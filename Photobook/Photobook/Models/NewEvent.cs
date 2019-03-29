using System;
namespace Photobook.Models
{
    public class NewEvent
    {
        public NewEvent()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventName { get; set; }
    }
}
