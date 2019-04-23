using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
                Guest testGuest = new Guest
                {
                    Pin = "2",
                    Username = "Troels"
                };
                IServerCommunicator com = new ServerCommunicator();

                await com.SendDataReturnIsValid(testGuest, DataType.Guest);

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
