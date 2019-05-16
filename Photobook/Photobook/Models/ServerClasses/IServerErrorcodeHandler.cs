using System.Net;
using System.Net.Http;

namespace Photobook.Models
{
    public interface IServerErrorcodeHandler
    {
        string Message { get; }
        void Handle(HttpResponseMessage msg);
    }

    public class GuestLoginErrorcodeHandler : IServerErrorcodeHandler
    {
        public void Handle(HttpResponseMessage msg)
        {
            switch (msg.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    Message = "Something went wrong. Try again later.";
                    break;
                case HttpStatusCode.NotFound:
                    Message = "Pin is wrong.";
                    break;
                case HttpStatusCode.NoContent:
                    Message = "Name is already taken.";
                    break;
                default:
                    Message = "Error logging in.";
                    break;
            }
        }

        public string Message { get; private set; } = "Error.";
    }
}