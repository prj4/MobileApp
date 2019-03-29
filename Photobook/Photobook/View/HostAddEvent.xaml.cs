using System;
using System.Collections.Generic;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostAddEvent : ContentPage
    {
        public HostAddEvent(User user)
        {
            var vm = new HostAddEventViewModel(user);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
