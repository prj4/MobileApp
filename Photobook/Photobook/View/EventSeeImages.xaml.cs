
using PB.Dto;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeImages : ContentPage
    {
        public EventSeeImages(EventModel _event)
        {
            InitializeComponent();
            var vm = new EventSeeImagesViewModel(_event);
            vm.Navigation = Navigation;

            BindingContext = vm;
            
        }

    }
}