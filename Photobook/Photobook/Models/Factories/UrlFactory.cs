using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class UrlFactory
    {

        private string NewUserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterHost";
        private string NewEventUrl = "https://photobookwebapi1.azurewebsites.net/api/Event/Create";
        private string UserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Login";
        private string ImageUploadUrl = "https://postman-echo.com/post";
        public string Generate(DataType d)
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
