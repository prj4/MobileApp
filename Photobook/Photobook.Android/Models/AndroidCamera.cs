﻿using Photobook.Droid.Models;
using Photobook.Models;
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
        

    }
}
