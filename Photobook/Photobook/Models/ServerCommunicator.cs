﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public interface IServerCommunicator
    {
        Task<bool> SendUserInfoReturnIsValid(User sender);
        Task<bool> SendPictureReturnSucces(string path);
    }

    public class ServerCommunicator : IServerCommunicator
    {
        private string Response { get; set; }
        private HttpClient client;
        private string UserServerUrl = "https://postman-echo.com/post";
        private string ImageUploadUrl = "https://postman-echo.com/post";
        public ServerCommunicator()
        {
            client = new HttpClient();
        }
        public async Task<bool> SendUserInfoReturnIsValid(User sender)
        {
            var data = JsonConvert.SerializeObject(sender);


            Debug.WriteLine(data + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

            var response = await client.PostAsync(UserServerUrl,
                new StringContent(data));

            response.EnsureSuccessStatusCode();
            
            Response = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(Response + DateTime.Now.ToString("ss.fff"), "SERVER_RESPONSE:");

            return false;
        }

        public async Task<bool> SendPictureReturnSucces(string path)
        {
            if (File.Exists(path))
            {
                
            }

            return false;
        }

    }
}
