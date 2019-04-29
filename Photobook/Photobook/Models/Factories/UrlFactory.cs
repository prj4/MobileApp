using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class UrlFactory
    {

        private static string NewHostServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Host";
        private static string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event";
        private static string GuestServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Guest";
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
                case DataType.Guest:
                {
                    return GuestServerUrl;
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
