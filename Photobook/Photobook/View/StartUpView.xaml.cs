﻿
using System.IO;
using Photobook.Models;
using Xamarin.Forms;

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
                string p = await DependencyService.Get<ICameraAPI>().TakePhotoReturnPath(); };
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
