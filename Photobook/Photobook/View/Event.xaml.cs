using System;
using System.Collections.Generic;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class Event : ContentPage
    {
        public Event()
        {
            var vm = new EventViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}
