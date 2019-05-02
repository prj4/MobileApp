using System;
using PB.Dto;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeImages : ContentPage
    {
        private EventSeeImagesViewModel vm;
        public EventSeeImages(EventModel _event)
        {
            vm = new EventSeeImagesViewModel(_event);
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
        }

        private void EventSeeImages_OnDisappearing(object sender, EventArgs e)
        {
            vm.OnLeave(sender, e);
        }
    }
}