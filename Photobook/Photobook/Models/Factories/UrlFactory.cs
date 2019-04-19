using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class UrlFactory
    {

        private static string NewHostServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Host";
        private static string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event/Create";
        private static string UserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterGuest";
        private static string ImageUploadUrl = "https://postman-echo.com/post";
        private static string HostLoginUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Login";
        private static string PictureUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture";
        public static string Generate(DataType d)
        {
            switch (d)
            {
                case DataType.NewUser:
                {
                    return NewHostServerUrl;
                }
                case DataType.User:
                {
                    return UserServerUrl;
                }
                case DataType.NewEvent:
                {
                    return NewEventUrl;
                }
                case DataType.Host:
                {
                    return HostLoginUrl;
                }
                case DataType.Picture:
                {
                    return PictureUrl;
                }
                default:
                {
                    return null;
                }
            }
        }
    }
}
