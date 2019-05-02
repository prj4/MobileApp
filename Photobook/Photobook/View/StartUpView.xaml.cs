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