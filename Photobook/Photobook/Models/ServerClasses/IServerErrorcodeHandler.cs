using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Photobook.Models
{
    public interface IServerErrorcodeHandler
    {
        void Handle(HttpResponseMessage msg);
        string Message { get; }
    }

    public class GuestLoginErrorcodeHandler : IServerErrorcodeHandler
    {
        public void Handle(HttpResponseMessage msg)
        {
            switch (msg.StatusCode)
            {
                case (HttpStatusCode.InternalServerError):
                    Message = "Something went wrong. Try again later.";
                    break;
                case (HttpStatusCode.NotFound):
                    Message = "Name already taken or pin is wrong.";
                    break;
                default:
                    Message = "Error logging in.";
                    break;
            }
        }

        public string Message { get; private set; } = "Error.";
    }
}
