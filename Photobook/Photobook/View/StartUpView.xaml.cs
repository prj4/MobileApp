using System;
using System.Diagnostics;
using System.IO;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            var vm = new StartUpViewViewModel(Navigation);
            //vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
        }
    }
}