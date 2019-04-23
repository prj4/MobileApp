using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models
{
    
    public class ParserFactory
    {
        public static IJSONParser Generate(DataType d)
        {
            switch (d)
            {
                case DataType.NewUser:
                {
                  return new NewHostParser();
                }
                case DataType.Guest:
                {
                    return new GuestParser();
                }
                case DataType.NewEvent:
                {
                    return new NewEventParser();
                }
                case DataType.Host:
                {
                    return new HostParser();
                }
                case DataType.Picture:
                {
                    return new PhotoParser();
                }
                default:
                    return null;
            }
        }
    }
}
