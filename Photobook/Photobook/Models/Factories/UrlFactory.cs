using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class UrlFactory
    {

        private static string NewUserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterHost";
        private static string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event/Create";
        private static string UserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterGuest";
        private static string ImageUploadUrl = "https://postman-echo.com/post";
        public static string Generate(DataType d)
        {
            switch (d)
            {
                case DataType.NewUser:
                {
                    return NewUserServerUrl;
                }
                case DataType.User:
                {
                    return UserServerUrl;
                }
                case DataType.NewEvent:
                {
                    return NewEventUrl;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
