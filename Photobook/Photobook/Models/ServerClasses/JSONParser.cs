using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public interface IJSONParser
    {
        string ParsedData(object o);
    }

    public class NewUserParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            User tmpUser;
            try
            {
                tmpUser = (User)u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }
            
            ServerNewUser newUser = new ServerNewUser
            {
                Name = tmpUser.Username,
                Email = tmpUser.Email,
                Password = tmpUser.Password
            };


            return JsonConvert.SerializeObject(newUser);
        }
    }

    public class HostParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            User tmpUser = new User();
            try
            {
                tmpUser = (User)u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }



            var content = new Dictionary<string, string>();
            content.Add("Name", "Oskar");
            content.Add("Password", "12345678");
            
            return JsonConvert.SerializeObject(content);
        }
    }

    public class UserParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            GuestUser tmpUser;
            try
            {
                tmpUser = (GuestUser) u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }

            ServerUser su = new ServerUser
            {
                Name = tmpUser.UserName,
                Pin = tmpUser.Pin
            };

            return JsonConvert.SerializeObject(su);

        }
    }

    public class NewEventParser : IJSONParser
    {
        public string ParsedData(object ne)
        {
            NewEvent newEvent;
            try
            {
                newEvent = (NewEvent) ne;
            }
            catch (Exception e)
            {
                return "";
            }

            return JsonConvert.SerializeObject(newEvent);
        }
    }
}
