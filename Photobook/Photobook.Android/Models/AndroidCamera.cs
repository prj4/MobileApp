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
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Photobook.Droid.Models;
using Photobook.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;


[assembly:Dependency(typeof(AndroidCamera))]
[Activity]
namespace Photobook.Droid.Models
{
    class AndroidCamera : ICameraAPI
    {
        private CameraManager Manager = null;
        public async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable
                || !CrossMedia.Current.IsPickPhotoSupported)
            {

            }

            var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
               SaveToAlbum = true
            });

            if (photo == null)
            {

            }
        }

    }
}
