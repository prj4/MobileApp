using System;
using System.Collections.Generic;
using System.IO.Compression;
using Photobook.Models;
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
            Button TestBtn = new Button();
            TestBtn.Text = "Troels' store testknap";
            string arg = "";
            TestBtn.Clicked += (sender, args) =>
            {
                DependencyService.Get<IUserServerCommunicator>().SendUserInformation();
                arg = DependencyService.Get<IUserServerCommunicator>().Result;
            };
            MainStack.Children.Add(TestBtn);
            DisplayAlert("New message", arg, "Ok");
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
