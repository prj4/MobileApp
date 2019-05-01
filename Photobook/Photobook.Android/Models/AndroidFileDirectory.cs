using System.IO;
using Android.OS;
using Android.Provider;
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
            string DownloadDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads)
                .AbsolutePath;
            string FullPath = DownloadDir + "/PhotobookPictures";
            Directory.CreateDirectory(FullPath);
            return FullPath;
        }

        public string GetTempPath()
        {
            string temp = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDocuments).AbsolutePath;
            string FullPath = temp + "/_tempFiles";
            Directory.CreateDirectory(FullPath);
            return FullPath;
        }
    }
}