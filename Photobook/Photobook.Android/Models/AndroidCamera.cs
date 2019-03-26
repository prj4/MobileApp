using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.Media.Audiofx;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.TextClassifiers;
using Android.Widget;
using Photobook.Droid.Models;
using Photobook.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;


[assembly:Dependency(typeof(AndroidCamera))]
namespace Photobook.Droid.Models
{
    class AndroidCamera : ICameraAPI
    {
        public async void TakePhoto()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            
        }

        public void SendInformation()
        {
           
        }

    }
}
