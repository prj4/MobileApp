using System;
using System.Collections.Generic;
using System.Text;
using Photobook.Models.ServerClasses;

namespace Photobook.Models.Factories
{
    static public class PostfixFactory
    {
        public static IUrlPostfix Generate(DataType d)
        {
            switch (d)
            {
                case (DataType.Guest):
                {
                    return new GuestPostfix();
                }
                case (DataType.DeleteEvent):
                {
                    return new EventPostFix();
                }
                case (DataType.Picture):
                {
                    return new PicturePostfix();
                }
                default:
                    return null;
            }
        }
    }
}
