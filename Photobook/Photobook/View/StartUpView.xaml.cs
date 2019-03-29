using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Photobook.Models;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Essentials;

namespace Photobook.View
{
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            InitializeComponent();

#if DEBUG
            Button TestBtn = new Button();
            TestBtn.Text = "Troels' store testknap";
            MainStack.Children.Add(TestBtn);

            TestBtn.Clicked += async (sender, args) =>
            {
                Location location = null;
                try
                {
                    location = await Geolocation.GetLastKnownLocationAsync();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                if (location != null)
                {
                    Debug.WriteLine($"Latitude: {location.Latitude}, longitude: {location.Longitude}");
                }
            };
#endif

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GuestLogin());
        }

        async void Host_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HostView());
        }

    }
}
