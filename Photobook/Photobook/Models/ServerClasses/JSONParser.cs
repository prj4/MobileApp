﻿using System;
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

    public class NewHostParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            Host tmpHost;
            try
            {
                tmpHost = (Host)u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }

            var content = new Dictionary<string, string>();
            content.Add("Name", tmpHost.Username);
            content.Add("Password", tmpHost.Password);
            content.Add("Email", tmpHost.Email);

            return JsonConvert.SerializeObject(content);
            
        }
    }

    public class HostParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            Host tmpHost = new Host();
            try
            {
                tmpHost = (Host)u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

            }

            var content = new Dictionary<string, string>();
            content.Add("UserName", tmpHost.Username);
            content.Add("Password", tmpHost.Password);

            return JsonConvert.SerializeObject(content);
        }
    }

    public class GuestParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            Guest tmpUser;
            try
            {
                tmpUser = (Guest) u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }

            var content = new Dictionary<string, string>();
            content.Add("UserName", tmpUser.UserName);
            content.Add("Pin", tmpUser.Pin);

            return JsonConvert.SerializeObject(content);

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
