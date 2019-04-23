using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class EventSeeSingleImageViewModel
    {
        public string ImageUrl;

        public EventSeeSingleImageViewModel(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
