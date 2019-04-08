using System;
namespace Photobook.Models
{
    public class NewEvent
    {
        public NewEvent(DateTime start, DateTime end, string name)
        {
            StartDate = start;
            EndDate = end;
            EventName = name;
        }

        public NewEvent()
        {

        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string EventName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

    }
}
