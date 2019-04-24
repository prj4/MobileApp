using System;
namespace Photobook.Models
{
    public class Event
    {
        public Event(DateTime start, DateTime end, string name)
        {
            StartDate = start;
            EndDate = end;
            Name = name;
        }

        public Event()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

    }
}
