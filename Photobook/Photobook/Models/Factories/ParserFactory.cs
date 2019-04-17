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
                case DataType.User:
                {
                    return new HostParser();
                }
                case DataType.NewEvent:
                {
                    return new NewEventParser();
                }
                case DataType.Host:
                {
                    return new HostParser();
                }
                default:
                    return null;
            }
        }
    }
}
