using System.Threading.Tasks;
using Android.Util;
using Photobook.Droid.Models;
using Photobook.Models;
using Xamarin.Forms;

[assembly:Dependency(typeof(AndroidMediaPicker))]
namespace Photobook.Droid.Models
{
    class AndroidMediaPicker : IMediaPicker
    {
        public async Task<string> GetPictureFromFolder()
        {
            var media = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();

            string ret = media != null ? media.Path : "Null";

            Log.Info("PickedPath", ret);

            return ret;
        }

        public async Task<string> GetVideoFromFolder()
        {
            var media = await Plugin.Media.CrossMedia.Current.PickVideoAsync();

            string ret = media != null ? media.Path : "Null";

            Log.Info("PickedPath", ret);

            return ret;
        }
    }
}