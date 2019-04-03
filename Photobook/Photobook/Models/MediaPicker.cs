using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace Photobook.Models
{
    interface IMediaPicker
    {
        Task<string> SelectPhoto();
        Task<string> SelectVideo();
    }

    public class CrossMediaPicker : IMediaPicker
    {
        public async Task<string> SelectPhoto()
        {
            var path = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {

            });

            string returnPath = (path == null) ? "Null" : path.AlbumPath;
            Debug.WriteLine(returnPath, "PHOTO_SELECTPATH");
            return returnPath;
        }

        public async Task<string> SelectVideo()
        {
            var path = await Plugin.Media.CrossMedia.Current.PickVideoAsync();

            string returnPath = (path == null) ? "Null" : path.AlbumPath;
            Debug.WriteLine(returnPath, "VIDEO_SELECTPATH");
            return returnPath;
        }

    }
}
