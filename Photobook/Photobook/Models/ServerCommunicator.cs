using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface IServerCommunicator
    {
        Task<bool> SendUserInformation(User sender);
        void UploadPhoto(string filePath);
        Task<bool> SendGuestLogon(GuestUser guest);

    }

    public class ServerCommunicator : IServerCommunicator
    {
        private string Response { get; set; }
        private HttpClient client;
        private const string HostLoginServerUrl = "https://postman-echo.com/post";
        private const string UploadPhotoServerUrl = "https://postman-echo.com/post";
        private const string GuestLoginServerUrl = "https://postman-echo.com/post";

        public ServerCommunicator()
        {
            client = new HttpClient();
        }

        public async Task<bool> SendGuestLogon(GuestUser guest)
        {
            var data = JsonConvert.SerializeObject(guest);
            var response = await client.PostAsync(GuestLoginServerUrl, new StringContent(data));
            if (response == null)
                return false;
            else
            {
                response.EnsureSuccessStatusCode();

                string serverResponse = await response.Content.ReadAsStringAsync();

                Debug.WriteLine(serverResponse, "SERVER_GUEST_MESSAGE");
                return false;
            }
        }
        
        public async Task<bool> SendUserInformation (User sender)
        {
            var data = JsonConvert.SerializeObject(sender);

            var response = await client.PostAsync(HostLoginServerUrl, new StringContent(data));

            if (response == null)
                return false;
            else
            {
                response.EnsureSuccessStatusCode();

                Response = await response.Content.ReadAsStringAsync();
                return false;
            }
            
        }

        public async void UploadPhoto(string filePath)
        {
            try
            {
                var FileBytes = File.ReadAllBytes(filePath);
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(FileBytes);

                string[] filePathSplit = filePath.Split('/');

                content.Add(baContent, "ImageFile", filePathSplit[filePathSplit.Length - 1]);

                var response = await client.PostAsync(UploadPhotoServerUrl, content);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine("UserServerCom exception: " + e.Message);
            }
        }
    }
}
