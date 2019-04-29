
using System;
using iOS.Models;
using Photobook.Models;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOSFileDirectory))]
namespace iOS.Models
{
    public class IOSFileDirectory : IFileDirectoryAPI
    {
        public string GetImagePath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}