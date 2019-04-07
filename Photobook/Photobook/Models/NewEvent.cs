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
        public TimeSpan StartTime { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan EndTime { get; set; }

        public string EventName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

    }
}
