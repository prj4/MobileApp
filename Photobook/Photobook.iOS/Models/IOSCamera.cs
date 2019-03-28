using System.Threading.Tasks;
using Photobook.Models;
using Plugin.Media.Abstractions;
using Xamarin.Forms.Internals;

namespace Photobook.iOS.Models
{
    class IOSCamera : ICameraAPI
    {
        private string PhotoPath = "Null";
        private string VideoPath = "Null";
        public async Task<string> TakePhotoReturnPath()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear
                });

            PhotoPath = photo != null ? photo.Path : "Null";

            return PhotoPath;
        }

        public async Task<string> TakeVideoReturnPath()
        {
            return VideoPath;
        }
    }
}