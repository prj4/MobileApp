using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostView : ContentPage
    {
        public HostView()
        {
            var vm = new HostViewViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}