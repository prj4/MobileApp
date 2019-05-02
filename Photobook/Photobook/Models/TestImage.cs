using System;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class TestImage
    {
        public string ImagePath { get; set; }

        public string FileName { get; set; }
        public ImageSource Source { get; set; }
        public string PinId { get; set; }
    }
}