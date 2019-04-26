using Android.OS;
using Photobook.Droid.Models;
using Photobook.Models;
using Xamarin.Forms;

[assembly:Dependency(typeof(AndroidFileDirectory))]
namespace Photobook.Droid.Models
{
    public class AndroidFileDirectory : IFileDirectoryAPI
    {
        public string GetImagePath()
        {
            
            return Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures).AbsolutePath;
        }
    }
}