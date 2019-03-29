using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface ICameraAPI
    {
        Task<string> TakePhotoReturnPath();
        Task<string> TakeVideoReturnPath();
    }

    class Camera : ICameraAPI
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
                    Name = $"photobook{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                });

            PhotoPath = photo != null ? photo.Path : "Null";
            Debug.WriteLine(PhotoPath, "Photopath");

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
            Debug.WriteLine(VideoPath, "VideoPath");

            return VideoPath;
        }

    }
}
