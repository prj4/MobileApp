using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

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

    public class UserParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            User tmpUser;
            try
            {
                tmpUser = (User) u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }

           
            ServerUser su = new ServerUser
            {
                UserName = tmpUser.Username,
                Password = tmpUser.Password
            };

            return JsonConvert.SerializeObject(su);

        }
    }
}
