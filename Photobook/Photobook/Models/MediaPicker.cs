using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Photobook.Models
{
    public interface IMediaPicker
    {
        Task<string> GetPictureFromFolder();
        Task<string> GetVideoFromFolder();
    }

}
    
