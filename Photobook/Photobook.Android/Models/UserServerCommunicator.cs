using System.Net.Http;
using Android.App;
using Android.Views;
using Android.Views.Accessibility;
using Photobook.Models;
using Photobook.Droid.Models;
using Photobook.View;
using Xamarin.Forms;

[assembly:Dependency(typeof(UserServerCommunicator))]
namespace Photobook.Droid.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
    }
    class UserServerCommunicator : IUserServerCommunicator
    {
        public string Result { get; set; }
        public async void SendUserInformation()
        {
            var client = new HttpClient();

            var hand = await client.GetAsync("https://example.com/hi/there?hand=wave");

            Result = hand.ToString();
        }
    }
}