using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Http;
using Org.Apache.Http.Client;
using Photobook.Models;
using Photobook.Droid.Models;
using Xamarin.Android.Net;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserServerCommunicator))]
namespace Photobook.Droid.Models
{
    class UserServerCommunicator : IUserServerCommunicator
    {
        public void SendUserInformation()
        {
            
            
        }
    }
}