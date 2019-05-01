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
        public HostMainMenu(Host host)
        {
            var vm = new HostMainMenuViewModel(host);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }

        public HostMainMenu(Host host, List<EventModel> sHost)
        {
            var vm = new HostMainMenuViewModel(host, sHost);
        }
    }
}