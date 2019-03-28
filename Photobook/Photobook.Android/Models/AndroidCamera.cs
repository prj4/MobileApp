﻿using System;
using System.Threading.Tasks;
using Android.Util;
using Photobook.Droid.Models;
using Photobook.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms;


[assembly:Dependency(typeof(AndroidCamera))]
namespace Photobook.Droid.Models
{
    class AndroidCamera : ICameraAPI
    {
        private string PhotoPath = "Test";
        private string VideoPath = "Test";
        public async Task<string> TakePhotoReturnPath()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear,
                    Name = $"Photobook{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                });

            PhotoPath = photo != null ? photo.Path : "Null";
            Log.Info("Photopath", PhotoPath);
        
            return PhotoPath;
        }

        public async Task<string> TakeVideoReturnPath()
        {
            var video = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(
                new Plugin.Media.Abstractions.StoreVideoOptions()
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear
                });

            VideoPath = video != null ? video.Path : "Null";
            Log.Info("VideoPath", VideoPath);

            return VideoPath;
        }
        
    }
}
