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
                ICameraAPI med = new CrossCamera();
                string path = await med.TakePhoto();
                IServerCommunicator Com = new ServerCommunicator();
                PhotoToServer ps = new PhotoToServer
                {
                    Path = path,
                    Pin = "2"
                };

                if (await Com.SendDataReturnIsValid(ps, DataType.Picture))
                {
                    Debug.WriteLine("Succes", "IMAGE_TO_SERVER");
                }
                else
                {
                    Debug.WriteLine("Failure", "IMAGE_TO_SERVER");
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
