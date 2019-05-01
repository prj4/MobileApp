using System.Collections.Generic;
using PB.Dto;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostMainMenu : ContentPage
    {
        public HostMainMenu(ReturnHostModel host)
        {
            var vm = new HostMainMenuViewModel(host);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }

        public HostMainMenu(ReturnHostModel host, List<EventModel> sHost)
        {
            var vm = new HostMainMenuViewModel(host, sHost);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }

        public HostMainMenu(Host _host)
        {
            var vm = new HostMainMenuViewModel(_host);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}