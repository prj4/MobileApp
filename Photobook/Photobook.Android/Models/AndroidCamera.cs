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
        public async Task<string> TakePhotoReturnPath()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(
                new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                {
                    SaveToAlbum = true,
                    DefaultCamera = CameraDevice.Rear
                });

            PhotoPath = photo != null ? photo.Path : "Null";
            Log.Info("Photopath", PhotoPath);
        
            return PhotoPath;
        }
        
    }
}
