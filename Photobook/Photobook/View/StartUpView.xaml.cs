using System;
using System.Collections.Generic;
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

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GuestLogin());
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            DependencyService.Get<ICameraAPI>().TakePhoto();
        }
    }
}
