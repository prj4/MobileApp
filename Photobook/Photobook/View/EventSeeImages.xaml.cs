using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeImages : ContentPage
    {
        public EventSeeImages(Event _event)
        {
            var vm = new EventSeeImagesViewModel(_event);
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
        }

    }
}