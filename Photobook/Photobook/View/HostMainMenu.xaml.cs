using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HostMainMenu : ContentPage
	{
		public HostMainMenu (User user)
		{
            var vm = new HostMainMenuViewModel(user);
            vm.Navigation = Navigation;
            BindingContext = vm;

			InitializeComponent ();
		}
	}
}