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
using Photobook.Models.ServerDataClasses;

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
        private IServerErrorcodeHandler errorHandler;
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

        public ServerCommunicator(IServerDataHandler _dataHandler, IServerErrorcodeHandler _errorHandler)
        {
            cookies = new CookieContainer();
            clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = cookies;

            client = new HttpClient(clientHandler);
            dataHandler = _dataHandler;
            errorHandler = _errorHandler;
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
                errorHandler?.Handle(response);

                Debug.WriteLine($"{e.Message}, {DateTime.Now.ToString("yy;MM;dd;HH;mm;ss")}",
                    "HttpRequestException");
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "Servercommunicator exception:");
                return false;
            }
#if DEBUG
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
#endif

            try
            {
                Uri cookieUrl =  new Uri(UrlFactory.Generate(dataType));
                var responseCookies = clientHandler.CookieContainer.GetCookies(cookieUrl);

                dataHandler.LatestMessage = response;
                dataHandler.LatestReceivedCookies = responseCookies;
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message, "CookieError");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<List<string>> GetImages(Photobook.Models.Event e)
        {
            clientHandler.CookieContainer.Add(SettingsManager.CurrentCookies);
            clientHandler.UseCookies = true;
            var response =
                await client.GetAsync(
                    "https://photobookwebapi1.azurewebsites.net/api/Picture/Ids" + $"/{e.Pin}");
            
            var rep = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(rep, "ImageResponse");
            if(String.IsNullOrEmpty(rep))
                return new List<string>();

            Debug.WriteLine(rep, "Images");
            RootObject result =
                JsonConvert.DeserializeObject<RootObject>(rep);

            return result.PictureList;
        }

      
    }
}
