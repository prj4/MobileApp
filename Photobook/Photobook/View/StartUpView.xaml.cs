using System;
using System.Diagnostics;
using System.IO;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            InitializeComponent();
#if DEBUG
            var btn = new Button();
            btn.Text = "Troels' store testknap";
            MainStack.Children.Add(btn);
            btn.Clicked += async (sender, args) =>
            {
                IServerDataHandler handler = new ServerDataHandler();
                IServerCommunicator com = new ServerCommunicator(handler);
                await com.SendDataReturnIsValid(
                    new Guest {Pin = "1234", Username = new Random().Next(100, 500).ToString()}
                    , DataType.Guest);

                IMediaDownloader downloader = new MediaDownloader(handler.LatestReceivedCookies);
                downloader.Downloading += e =>
                {
                    if (e.StatusOk)
                    {
                        Debug.WriteLine(DependencyService.Get<IFileDirectoryAPI>().GetImagePath() + "/img2.PNG");
                        File.WriteAllBytes(DependencyService.Get<IFileDirectoryAPI>().GetImagePath() + "/img2.PNG",
                            e.FileBytes);
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

        private async void Handle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GuestLogin());
        }

        private async void Host_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HostView());
        }
    }
}