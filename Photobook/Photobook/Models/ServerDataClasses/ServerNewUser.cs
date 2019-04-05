using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Photobook.Models
{
    public class ServerNewUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
