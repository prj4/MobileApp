using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photobook.Models.ServerClasses;

namespace Photobook.Models
{
    public enum DataType
    {
        NewUser,
        Guest,
        NewEvent,
        Host,
        Picture,
        Video,
    };
    
    public interface IServerCommunicator
    {
        Task<bool> SendDataReturnIsValid(object o, DataType d);
        void AddCookies(CookieCollection _cookies);

    }

    public class ServerCommunicator : IServerCommunicator
    {

        private string Response;
        private HttpClient client;
        private CookieContainer cookies;
        private HttpClientHandler clientHandler;
        private IServerDataHandler dataHandler;
        public ServerCommunicator()
        {
            cookies = new CookieContainer();
            clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = cookies;

            client = new HttpClient(clientHandler);
            dataHandler = new ServerDataHandler();
        }

        public ServerCommunicator(IServerDataHandler _dataHandler)
        {
            cookies = new CookieContainer();
            clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = cookies;

            client = new HttpClient(clientHandler);
            dataHandler = _dataHandler;
        }

        public void AddCookies(CookieCollection _cookies)
        {
            
            cookies.Add(_cookies);
        }

        public async Task<bool> SendDataReturnIsValid(object dataToSend, DataType dataType)
        {
            IJSONParser parser = ParserFactory.Generate(dataType);
            var data = parser.ParsedData(dataToSend);

            if(!(dataType == DataType.Picture || dataType == DataType.Video))
                Debug.WriteLine(data + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

            if (cookies.Count > 0)
                clientHandler.UseCookies = true;

            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(UrlFactory.Generate(dataType),
                    new StringContent(data, Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"{e.Message}, {DateTime.Now.ToString("yy;MM;dd;HH;mm;ss")}",
                    "HttpRequestException");
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "Servercommunicator exception:");
                return false;
            }

            try
            {
                Response = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
            
            Debug.WriteLine(Response + DateTime.Now.ToString("ss.fff"), "SERVER_RESPONSE:");

            try
            {
                var responseCookies = clientHandler.CookieContainer.GetCookies(new Uri(UrlFactory.Generate(dataType)));

                dataHandler.LatestMessage = response;
                dataHandler.LatestReceivedCookies = responseCookies;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message, "CookieError");
            }

            return response.IsSuccessStatusCode;
        }

        public async void GetImages()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("eventPin", "2");

            string toServer = JsonConvert.SerializeObject(dic);

            var response =
                await client.GetAsync(
                    "https://photobookwebapi1.azurewebsites.net/api/Picture/Ids" + '/' + $"{toServer}");
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
        }

      
    }
}
