using System;
namespace Photobook.Models
{
    public class NewEvent
    {
        public NewEvent(DateTime Start, DateTime end, string name)
        {
            StartDate = Start;
            EndDate = end;
            EventName = name;
        }

        public NewEvent()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventName { get; set; }
    }
}
