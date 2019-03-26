using System;
using System.Collections.Generic;
using System.Text;

namespace Photobook.Models
{
    public interface IUserServerCommunicator
    {
        string Result { get; set; }
        void SendUserInformation();
    }
}
