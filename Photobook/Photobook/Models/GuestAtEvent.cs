using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Photobook.Models
{
    public class GuestAtEvent
    {
        public Guest GuestInfo { get; set; }
        public Event EventInfo { get; set; }
        //public CookieCollection CookieInfo { get; set; }
        public string Presentation
        {
            get { return $"{EventInfo.Name} - {GuestInfo.Username}, ends {EventInfo.EndDate}";}
            private set
            {

            }
        }
    }
}
