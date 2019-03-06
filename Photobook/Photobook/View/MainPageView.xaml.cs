using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;


namespace Photobook.View
{
    public partial class MainPageView : ContentPage
    {

        public MainPageView()
        {
            InitializeComponent();

            BindingContext = new UserViewmodel();

        }
    }
}
