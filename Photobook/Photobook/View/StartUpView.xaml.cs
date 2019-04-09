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
            btn.Clicked += (sender, args) =>
            {
                IServerCommunicator com = new ServerCommunicator();
                NewEvent e = new NewEvent{
                    Description = "Her er der fest",
                    Location = "Hos Oskar",
                    StartDate = DateTime.ParseExact("20190409", "yyyyMMdd", CultureInfo.InvariantCulture),
                    EndDate =  DateTime.ParseExact("20190410", "yyyyMMdd", CultureInfo.InvariantCulture),
                    Name = "Stor fest"
                };

                com.SendDataReturnIsValid(e, DataType.NewEvent);
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
