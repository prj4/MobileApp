using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface ICameraAPI
    {
        void Open();

    }

    public class AndroidCamera : ICameraAPI
    {
        public void Open()
        {
            
            
        }
    }
}
