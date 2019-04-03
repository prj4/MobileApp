using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface ICameraAPI
    {
        Task<string> TakePhoto();
        Task<string> TakeVideo();
    }

    public class CrossCamera : ICameraAPI
    {
        public async Task<string> TakeVideo()
        {
            var video = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear
                });

            string path = (video == null) ? "Null" : video.AlbumPath;
            Debug.WriteLine(path, "VIDEOPATH");
            return path;
        }
        public async Task<string> TakePhoto()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear
                });

            string path = (photo == null) ? "Null" : photo.AlbumPath;

            Debug.WriteLine(path, "PHOTOPATH");
            return path;
        }
    }
}
