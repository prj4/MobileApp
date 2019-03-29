using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostAddEvent : ContentPage
    {
        public HostAddEvent(User user, ref NewEvent newEvent)
        {
            var vm = new HostAddEventViewModel(user, ref newEvent);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
