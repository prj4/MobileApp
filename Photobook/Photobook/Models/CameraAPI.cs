using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface ICameraAPI
    {
        Task<string> TakePhotoReturnPath();
        Task<string> TakeVideoReturnPath();
    }
}
