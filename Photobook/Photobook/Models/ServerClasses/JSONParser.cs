using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using PB.Dto;

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
                tmpHost = (Host) u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "";
            }

            var content = new Dictionary<string, string>();
            content.Add("Name", tmpHost.Name);
            content.Add("Password", tmpHost.Password);
            content.Add("Email", tmpHost.Email);

            return JsonConvert.SerializeObject(content);
        }
    }

    public class PhotoParser : IJSONParser
    {
        public string ParsedData(object f)
        {
            var ps = (PhotoToServer) f;

            var bytes = File.ReadAllBytes(ps.Path);
            var asString = Convert.ToBase64String(bytes);

            var content = new Dictionary<string, string>();
            content.Add("pictureString", asString);
            content.Add("eventPin", ps.Pin);

            return JsonConvert.SerializeObject(content);
        }
    }

    public class HostParser : IJSONParser
    {
        public string ParsedData(object u)
        {
            var tmpHost = new Host();
            try
            {
                tmpHost = (Host) u;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            var content = new Dictionary<string, string>();
            content.Add("UserName", tmpHost.Email);
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
            content.Add("Name", tmpUser.Username);
            content.Add("Pin", tmpUser.Pin);

            return JsonConvert.SerializeObject(content);
        }
    }

    public class NewEventParser : IJSONParser
    {
        public string ParsedData(object ne)
        {
            var newEvent = new EventModel();
            try
            {
                newEvent = (EventModel) ne;
            }
            catch (Exception e)
            {
                return "";
            }

            var content = new Dictionary<string, string>();
            content.Add("Name", newEvent.Name);
            content.Add("Description", newEvent.Description);
            content.Add("Location", newEvent.Location);
            content.Add("StartDate", newEvent.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"));
            content.Add("EndDate", newEvent.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"));

            return JsonConvert.SerializeObject(content);
        }
    }
}