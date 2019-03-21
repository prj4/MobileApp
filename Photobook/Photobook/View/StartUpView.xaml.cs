using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
    }
}
