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
        public HostAddEvent(User user, ObservableCollection<NewEvent> events)
        {
            var vm = new HostAddEventViewModel(user, events);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
