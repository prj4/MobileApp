using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PB.Dto;
using Photobook.Models.Factories;
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
        DeleteEvent,
        GetPicture,
        GetPreview
    }

    public interface IServerCommunicator
    {
        Task<bool> SendDataReturnIsValid(object o, DataType d);
        void AddCookies(CookieCollection _cookies);
        Task<bool> DeleteFromServer(object o, DataType d);
        Task<List<string>> GetImages(EventModel e, CookieCollection c);
    }

    public class ServerCommunicator : IServerCommunicator
    {
        private readonly HttpClient client;
        private readonly HttpClientHandler clientHandler;
        private readonly CookieContainer cookies;
        private readonly IServerDataHandler dataHandler;
        private readonly IServerErrorcodeHandler errorHandler;

        private string Response;

        public ServerCommunicator(HttpClient _client, HttpClientHandler _clientHandler,
            CookieContainer _cookies, IServerDataHandler _dataHandler, IServerErrorcodeHandler _errorHandler)
        {
            client = _client;
            clientHandler = _clientHandler;
            cookies = _cookies;
            dataHandler = _dataHandler;
            errorHandler = _errorHandler;
        }
        public ServerCommunicator()
        {
            cookies = new CookieContainer();
            clientHandler = new HttpClientHandler();
            clientHandler.CookieContainer = cookies;

            client = new HttpClient(clientHandler);
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
            cookies?.Add(_cookies);
        }

        public async Task<bool> SendDataReturnIsValid(object dataToSend, DataType dataType)
        {
            var parser = ParserFactory.Generate(dataType);
            var data = parser.ParsedData(dataToSend);

            if (!(dataType == DataType.Picture || dataType == DataType.Video))
                Debug.WriteLine(data + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

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

                Debug.WriteLine($"{e.Message}",
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

            Debug.WriteLine(Response, "SERVER_RESPONSE:");
#endif

            try
            {
                var cookieUrl = new Uri(UrlFactory.Generate(dataType));
                var responseCookies = clientHandler.CookieContainer.GetCookies(cookieUrl);

                if (dataHandler != null)
                {
                    dataHandler.LatestMessage = response;
                    dataHandler.LatestReceivedCookies = responseCookies;
                }
                
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message, "CookieError");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<List<string>> GetImages(EventModel e, CookieCollection cookie)
        {
            clientHandler.CookieContainer.Add(cookie);
            clientHandler.UseCookies = true;
            var response =
                await client.GetAsync(
                    "https://photobookwebapi1.azurewebsites.net/api/Picture/Ids" + $"/{e.Pin}");

            var rep = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(rep, "ImageResponse");
            if (string.IsNullOrEmpty(rep))
                return new List<string>();

            Debug.WriteLine(rep, "Images");
            var result =
                JsonConvert.DeserializeObject<PictureListModel>(rep);

            return result.PictureList;
        }

        public async Task<bool> DeleteFromServer(object o, DataType d)
        {
            clientHandler.UseCookies = true;
            var postfixCreator = PostfixFactory.Generate(d);
            var postfix = postfixCreator.Generate(o);

            var rep = await client.DeleteAsync($"{UrlFactory.Generate(d)}/{postfix}");

            return rep.IsSuccessStatusCode;
        }
    }
}