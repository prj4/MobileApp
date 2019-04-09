using System;
namespace Photobook.Models
{
    public class NewEvent
    {
        public NewEvent(DateTime start, DateTime end, string name)
        {
            StartDate = start;
            EndDate = end;
            Name = name;
        }

        public NewEvent()
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

    }
}
