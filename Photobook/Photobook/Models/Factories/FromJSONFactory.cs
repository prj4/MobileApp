using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models
{
    public enum ServerData
    {
        Event,
        Host
    }
    public class FromJSONFactory
    {
        public static IFromJSONParser Generate(ServerData d)
        {
            switch (d)
            {
                case ServerData.Event:
                {
                    return new EventFromJSONParser();
                }
                case ServerData.Host:
                {
                    return new HostFromJSONParser();
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
