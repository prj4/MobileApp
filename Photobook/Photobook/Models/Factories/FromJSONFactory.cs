using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models
{
    public enum ServerData
    {
        Event
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
                default:
                {
                    return null;
                }
            }
        }
    }
}
