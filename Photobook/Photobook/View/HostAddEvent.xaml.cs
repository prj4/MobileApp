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
        public HostAddEvent(Host host, ObservableCollection<Event> events)
        {
            var vm = new HostAddEventViewModel(host, events);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
