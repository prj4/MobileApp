using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Photobook.Models
{
    public interface IUserServerCommunicator
    {
        void SendUserInformation(User sender);
    }

    public class UserServerCommunicator : Page, IUserServerCommunicator
    {
        public async void SendUserInformation (User sender)
        {
            var client = new HttpClient();
            var data = JsonConvert.SerializeObject(sender);

            var response = await client.PostAsync("https://postman-echo.com/post",
                new StringContent(data));

            response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();
        }
    }
}
