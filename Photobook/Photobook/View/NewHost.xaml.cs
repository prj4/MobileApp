using System;
using System.Collections.Generic;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class NewHost : ContentPage
    {
        public NewHost(NewHostViewModel vm)
        {
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();

        }
    }
}
