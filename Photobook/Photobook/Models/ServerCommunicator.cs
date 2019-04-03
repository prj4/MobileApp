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
    public class ServerNewUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ServerUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public interface IServerCommunicator
    {
        Task<bool> SendNewUserInfoReturnIsValid(User sender);
        Task<bool> SendPictureReturnSucces(string path);
        Task<bool> SendLoginReturnSucces(User sender);
    }

    public class ServerCommunicator : IServerCommunicator
    {
        private string Response { get; set; }
        private HttpClient client;
        private string NewUserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/RegisterHost";
        private string UserServerUrl = "https://photobookwebapi1.azurewebsites.net/api/Account/Login";
        private string ImageUploadUrl = "https://postman-echo.com/post";
        public ServerCommunicator()
        {
            client = new HttpClient();
        }

        public async Task<bool> SendLoginReturnSucces(User sender)
        {
            ServerUser su = new ServerUser
            {
                UserName = sender.Username,
                Password = sender.Password
            };

            var data = JsonConvert.SerializeObject(su);


            return await SendJSON(data, UserServerUrl);
        }
        public async Task<bool> SendNewUserInfoReturnIsValid(User sender)
        {
            ServerNewUser su = new ServerNewUser
            {
                Name = sender.Username,
                Email = sender.Email,
                Password = sender.Password,
                ConfirmPassword = sender.Password
            };
            var data = JsonConvert.SerializeObject(su);


            return await SendJSON(data, NewUserServerUrl);
        }

        public async Task<bool> SendPictureReturnSucces(string path)
        {
            if (File.Exists(path))
            {
                
            }

            return false;
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
                Debug.WriteLine($"{e.Message}, {DateTime.Now.ToString("yyMMddHHmmss")}",
                    "HttpRequestException");
                return false;
            }
        }

    }
}
