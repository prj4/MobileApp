using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.Models
{
    public interface IUserServerCommunicator
    {
        bool SendUserInfoReturnIsValid(User sender);
        void UploadPhoto(string filePath);

    }

    public class UserServerCommunicator : IUserServerCommunicator
    {
        private string Response { get; set; }
        private HttpClient client;
        private string LoginServerUrl = "https://postman-echo.com/post";
        private string UploadPhotoServerUrl = "https://postman-echo.com/post";

        public UserServerCommunicator()
        {
            client = new HttpClient();
        }
        public bool SendUserInfoReturnIsValid(User sender)
        {
            SendUserInformation(sender);
            return Response == null;
        }
        private async void SendUserInformation (User sender)
        {
            var data = JsonConvert.SerializeObject(sender);

            var response = await client.PostAsync(LoginServerUrl, new StringContent(data));

            response.EnsureSuccessStatusCode();

            Response = await response.Content.ReadAsStringAsync();

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
                
                Debug.WriteLine("UserServerCom response: " + response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("UserServerCom exception: " + e.Message);
            }
        }
    }
}
