using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class GuestLogin : ContentPage
    {
        public GuestLogin()
        {
            var vm = new GuestLoginViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;

            InitializeComponent();
        }
    }
}