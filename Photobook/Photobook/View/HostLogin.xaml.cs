using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class HostLogin : ContentPage
    {
        public HostLogin()
        {
            var vm = new HostLoginViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
        }
    }
}