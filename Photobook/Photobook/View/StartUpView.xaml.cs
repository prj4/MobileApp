using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using PCLStorage;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using FileSystem = Xamarin.Essentials.FileSystem;

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
                IServerDataHandler handler = new ServerDataHandler();
                IServerCommunicator com = new ServerCommunicator(handler);
                await com.SendDataReturnIsValid(new Guest { Pin = "1234", Username = new Random().Next(100, 500).ToString() }
                    , DataType.Guest);

                IMediaDownloader downloader = new MediaDownloader(handler.LatestReceivedCookies);
                downloader.DownloadReady += (e) =>
                {
                    if (e.StatusOk)
                    {
                        Debug.WriteLine(DependencyService.Get<IFileDirectoryAPI>().GetImagePath() + "/img2.PNG");
                        File.WriteAllBytes(DependencyService.Get<IFileDirectoryAPI>().GetImagePath() + "/img2.PNG", e.FileBytes);
                        
                    }
                    else
                    {
                        Debug.WriteLine("Error");
                    }
                };

                
                downloader.DownloadSingleImage("https://photobookwebapi1.azurewebsites.net/api/Picture/1234/22");


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
