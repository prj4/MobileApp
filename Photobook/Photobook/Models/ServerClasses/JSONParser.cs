using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

    public class PhotoParser : IJSONParser
    {
        public string ParsedData(object f)
        {
            PhotoToServer ps = (PhotoToServer) f;

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
            Event newEvent = new Event();
            try
            {
                newEvent = (Event) ne;
            }
            catch (Exception e)
            {
                return "";
            }

            var content = new Dictionary<string, string>();
            content.Add("Name", newEvent.Name);
            content.Add("Description", newEvent.Description);
            content.Add("Location", newEvent.Location);
            content.Add("StartDate", newEvent.StartDate.ToString("yyyyMMddHHmm"));
            content.Add("EndDate", newEvent.EndDate.ToString("yyyyMMddHHmm"));

            return JsonConvert.SerializeObject(content);
        }
    }
}
