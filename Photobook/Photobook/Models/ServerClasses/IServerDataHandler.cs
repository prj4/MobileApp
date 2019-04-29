using System.Net;
using System.Net.Http;

namespace Photobook.Models.ServerClasses
{
    public interface IServerDataHandler
    {
        HttpResponseMessage LatestMessage { get; set; }
        CookieCollection LatestReceivedCookies { get; set; }
    }

    public class ServerDataHandler : IServerDataHandler
    {
        public CookieCollection LatestReceivedCookies { get; set; }
        public HttpResponseMessage LatestMessage { get; set; }
    }
}