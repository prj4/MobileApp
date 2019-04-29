using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photobook.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeImages : ContentPage
    {
        private readonly HttpClient _client;

        public EventSeeImages()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var images = await GetImageListAsync();
            if (images != null)
                foreach (var photo in images.Photos)
                {
                    var image = new Image
                    {
                        Source = ImageSource.FromUri(new Uri(photo + string.Format("?width={0}&height={0}&mode=max",
                                                                 Device.RuntimePlatform == Device.UWP ? 120 : 240)))
                    };
                    wrapLayout.Children.Add(image);
                }
        }

        private async Task<ImageList> GetImageListAsync()
        {
            try
            {
                var requestUri = "https://docs.xamarin.com/demo/stock.json";
                var result = await _client.GetStringAsync(requestUri);
                return JsonConvert.DeserializeObject<ImageList>(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"\tERROR: {ex.Message}");
            }

            return null;
        }
    }
}