using System;
using System.Collections.Generic;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostView : ContentPage
    {
        public HostView()
        {
            InitializeComponent();
        }

        async void NewUser_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NewUser(new NewUserViewModel()));
        }
    }
}
