using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Photobook.Models;

namespace Photobook.Droid.Models
{
    class AndroidCamera : ICameraAPI
    {
        public void Open()
        {
            Camera.Open();
            
        }
    }
}