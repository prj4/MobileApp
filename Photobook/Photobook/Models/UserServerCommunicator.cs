using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public interface IUserServerCommunicator
    {
        bool SendUserInfoReturnIsValid(User sender);

    }

    public class UserServerCommunicator : IUserServerCommunicator
    {
        private string Response { get; set; }
        public bool SendUserInfoReturnIsValid(User sender)
        {
            SendUserInformation(sender);
            return Response == null;
        }
        private async void SendUserInformation (User sender)
        {
            var client = new HttpClient();
            var data = JsonConvert.SerializeObject(sender);

            var response = await client.PostAsync("https://postman-echo.com/post",
                new StringContent(data));

            response.EnsureSuccessStatusCode();

            Response = await response.Content.ReadAsStringAsync();

        }
    }
}
