using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Http;
using Newtonsoft.Json;
using Photobook.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Photobook.View
{
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            InitializeComponent();
#if DEBUG
            Button btn = new Button();
            btn.Text = "Troels' store testknap";
            MainStack.Children.Add(btn);
            btn.Clicked += async (sender, args) =>
            {
                string[] data = await GeoData.GetCurrentLocation();

                try
                {

                    var message = new SmsMessage($"Hello Oskar! This is the automated SMS bot. Troels' current specific location is {data[0]} and {data[1]}", "60148066");
                    await Sms.ComposeAsync(message);
                    
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
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
