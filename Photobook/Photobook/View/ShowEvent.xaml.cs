using System;
using System.Collections.Generic;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class ShowEvent : ContentPage
    {
        public ShowEvent(Event newEvent, bool ShowTopBar)
        {
            var vm = new ShowEventViewModel(newEvent, ShowTopBar);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }

        public ShowEvent(Event newEvent, Guest currentGuest, bool ShowTopBar)
        {
            var vm = new ShowEventViewModel(newEvent, currentGuest, ShowTopBar);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
