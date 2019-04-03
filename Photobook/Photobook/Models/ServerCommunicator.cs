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
    public class ServerUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public interface IServerCommunicator
    {
        Task<bool> SendUserInfoReturnIsValid(User sender);
        Task<bool> SendPictureReturnSucces(string path);
    }

    public class ServerCommunicator : IServerCommunicator
    {
        private string Response { get; set; }
        private HttpClient client;
        private string UserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterHost";
        private string ImageUploadUrl = "https://postman-echo.com/post";
        public ServerCommunicator()
        {
            client = new HttpClient();
        }
        public async Task<bool> SendUserInfoReturnIsValid(User sender)
        {
            ServerUser su = new ServerUser
            {
                Name = sender.Username,
                Email = sender.Email,
                Password = sender.Password,
                ConfirmPassword = sender.Password
            };
            var data = JsonConvert.SerializeObject(su);
            


            Debug.WriteLine(data + DateTime.Now.ToString("ss.fff"), "JSON_DATA:");

            var response = await client.PostAsync(UserServerUrl,
                new StringContent(data, Encoding.UTF8, "application/json"));

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
