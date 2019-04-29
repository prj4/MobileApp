using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace Photobook.Models
{
    public interface ICameraAPI
    {
        Task<string> TakePhoto();
        Task<string> TakeVideo();
    }

    public class CrossCamera : ICameraAPI
    {
        private readonly string DateTimeFormat = "yyMMddHHmmssfff";

        public async Task<string> TakeVideo()
        {
            var video = await CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear,
                    Name = $"photobook{DateTime.Now.ToString(DateTimeFormat)}"
                });

            var path = video == null ? "Null" : video.AlbumPath;
            Debug.WriteLine(path, "VIDEOPATH");
            return path;
        }

        public async Task<string> TakePhoto()
        {
            var photo = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear,
                    Name = $"photobook{DateTime.Now.ToString(DateTimeFormat)}"
                });

            var path = photo == null ? "Null" : photo.AlbumPath;

            Debug.WriteLine(path, "PHOTOPATH");
            return path;
        }
    }
}