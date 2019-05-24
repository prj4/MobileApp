using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSeeSingleImage : ContentPage
    {
        public EventSeeSingleImage(TestImage img)
        {
            var vm = new EventSeeSingleImageViewModel(img);
            vm.Navigation = Navigation;
            this.BindingContext = vm;
            InitializeComponent();
        }
    }
}