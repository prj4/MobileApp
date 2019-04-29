using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeImages : ContentPage
    {
        
        public EventSeeImages(Event loadEvent)
        {
            var vm = new EventSeeImagesViewModel(loadEvent);
            vm.Navigation = Navigation;
            BindingContext = vm;
            
            InitializeComponent();
        }
    }
}