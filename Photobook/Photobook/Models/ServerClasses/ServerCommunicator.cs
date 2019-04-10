using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public enum DataType
    {
        NewUser,
        User,
        NewEvent,
        Picture,
        Video
    };
    
    public interface IServerCommunicator
    {
        ServerResponse ResponseObject { get; }
        Task<bool> SendDataReturnIsValid(object o, DataType d);
        Task<ServerResponse> SendDataReturnResponse(object o, DataType d);
    }

    public class ServerCommunicator : IServerCommunicator
    {

        private string Response;
        public ServerResponse ResponseObject { get; private set; }
        private ParserFactory parsFactory;
        private UrlFactory urlFactory;
        private HttpClient client;
        private CookieContainer cookies;
        private HttpClientHandler handler;
        public ServerCommunicator()
        {
            
            cookies = new CookieContainer();
            handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            client = new HttpClient(handler);
            parsFactory = new ParserFactory();
            urlFactory = new UrlFactory();
        }

        public async Task<bool> SendDataReturnIsValid(object dataToSend, DataType dataType)
        {
            IJSONParser parser = parsFactory.Generate(dataType);
            var data = parser.ParsedData(dataToSend);
            
            
            return await SendJSON(data, urlFactory.Generate(dataType));
        }

        public async Task<ServerResponse> SendDataReturnResponse(object o, DataType d)
        {
            IJSONParser parser = parsFactory.Generate(d);
            var data = parser.ParsedData(o);

            return await (SendJSON(data, urlFactory.Generate(d))) ? ResponseObject : null;
        }
            

        private async Task<bool> SendJSON(string JSON, string Url)
        {
            Debug.WriteLine(JSON + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");
            ResponseObject = null;

            var response = await client.PostAsync(Url,
                new StringContent(JSON, Encoding.UTF8, "application/json"));
            
            var cookies = handler.CookieContainer.GetCookies(new Uri(Url));

            foreach (var cookie in cookies)
            {
                Debug.WriteLine(cookie.ToString(), "COOKIE");
            }

            try
            {
                ResponseObject = JsonConvert.DeserializeObject<ServerResponse>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "Deserialize exception");
            }
            

            try
            {
                response.EnsureSuccessStatusCode();
                Response = await response.Content.ReadAsStringAsync();

                Debug.WriteLine(Response + DateTime.Now.ToString("ss.fff"), "SERVER_RESPONSE:");
                return true;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"{e.Message}, {DateTime.Now.ToString("yy;MM;dd;HH;mm;ss")}",
                    "HttpRequestException");
                return false;
            }
        }

    }
}
