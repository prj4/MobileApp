using System.Collections.ObjectModel;
using PB.Dto;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostAddEvent : ContentPage
    {
        public HostAddEvent(Host host, ObservableCollection<EventModel> events)
        {
            var vm = new HostAddEventViewModel(host, events);
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}