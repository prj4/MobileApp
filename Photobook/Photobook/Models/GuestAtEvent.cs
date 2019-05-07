using PB.Dto;

namespace Photobook.Models
{
    public class GuestAtEvent
    {
        public Guest GuestInfo { get; set; }

        public EventModel EventInfo { get; set; }

        //public CookieCollection CookieInfo { get; set; }
        public string Presentation
        {
            get
            {
                return $"{EventInfo.Name} - {GuestInfo.Username}, ends {EventInfo.EndDate.ToString("f")}";
            }
        }
    }
}