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
                  return new NewUserParser();
                }
                case DataType.User:
                {
                    return new UserParser();
                }
                case DataType.NewEvent:
                {
                    return new NewEventParser();
                }
                default:
                    return null;
            }
        }
    }
}
