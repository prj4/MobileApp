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
            var vm = new EventViewModel(newEvent, ShowTopBar);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
