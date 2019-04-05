using System;
using System.Diagnostics;
using System.IO;
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
        Task<bool> SendDataReturnIsValid(object o, DataType d);
    }

    public class ServerCommunicator : IServerCommunicator
    {
        
        private string Response { get; set; }
        private ParserFactory parsFactory;
        private UrlFactory urlFactory;
        private HttpClient client;
        public ServerCommunicator()
        {
            client = new HttpClient();
            parsFactory = new ParserFactory();
            urlFactory = new UrlFactory();
        }

        public async Task<bool> SendDataReturnIsValid(object dataToSend, DataType dataType)
        {
            IJSONParser parser = parsFactory.Generate(dataType);
            var data = parser.ParsedData(dataToSend);

            return await SendJSON(data, urlFactory.Generate(dataType));
        }

       

        private async Task<bool> SendJSON(string JSON, string Url)
        {
            Debug.WriteLine(JSON + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

            var response = await client.PostAsync(Url,
                new StringContent(JSON, Encoding.UTF8, "application/json"));

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
