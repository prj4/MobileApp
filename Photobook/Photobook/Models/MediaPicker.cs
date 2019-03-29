using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public interface IMediaPicker
    {
        Task<string> GetPictureFromFolder();
        Task<string> GetVideoFromFolder();
    }
    class AndroidMediaPicker : IMediaPicker
    {
        public async Task<string> GetPictureFromFolder()
        {
            var media = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();

            string ret = media != null ? media.Path : "Null";

            Debug.WriteLine(ret, "Pickedpath");

            return ret;
        }

        public async Task<string> GetVideoFromFolder()
        {
            var media = await Plugin.Media.CrossMedia.Current.PickVideoAsync();

            string ret = media != null ? media.Path : "Null";

            Debug.WriteLine(ret, "Pickedpath");

            return ret;
        }
    }
}
    
