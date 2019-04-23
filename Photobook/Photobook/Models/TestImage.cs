using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class TestImage
    {
        public string ImageUrl { get; set; }
        public ImageSource ImgUri = ImageSource.FromUri(new Uri("https://photobookwebapi1.azurewebsites.net/api/Picture/rine2164bk/4"));
    }
}
