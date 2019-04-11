﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photobook.Models.ServerClasses;
using Xamarin.Forms;

namespace Photobook.Models
{
    public enum DataType
    {
        NewUser,
        User,
        NewEvent,
        Picture,
        Video,
    };
    
    public interface IServerCommunicator
    {
        Task<bool> SendDataReturnIsValid(object o, DataType d);
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

        public async Task<bool> SendDataReturnIsValid(object dataToSend, DataType dataType)
        {
            IJSONParser parser = ParserFactory.Generate(dataType);
            var data = parser.ParsedData(dataToSend);

            Debug.WriteLine(data + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

            var response = await client.PostAsync(UrlFactory.Generate(dataType),
                new StringContent(data, Encoding.UTF8, "application/json"));

            var cookies = clientHandler.CookieContainer.GetCookies(new Uri(UrlFactory.Generate(dataType)));

            dataHandler.LatestMessage = response;
            dataHandler.LatestReceivedCookies = cookies;

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
