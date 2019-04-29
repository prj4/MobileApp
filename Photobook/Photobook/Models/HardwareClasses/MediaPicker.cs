using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Media;

namespace Photobook.Models
{
    internal interface IMediaPicker
    {
        Task<string> SelectPhoto();
        Task<string> SelectVideo();
    }

    public class CrossMediaPicker : IMediaPicker
    {
        public async Task<string> SelectPhoto()
        {
            await CrossMedia.Current.Initialize();
            var path = await CrossMedia.Current.PickPhotoAsync();

            var returnPath = path == null ? "Null" : path.Path;
            Debug.WriteLine(returnPath, "PHOTO_SELECTPATH");
            return returnPath;
        }

        public async Task<string> SelectVideo()
        {
            var path = await CrossMedia.Current.PickVideoAsync();

            var returnPath = path == null ? "Null" : path.Path;
            Debug.WriteLine(returnPath, "VIDEO_SELECTPATH");
            return returnPath;
        }
    }
}