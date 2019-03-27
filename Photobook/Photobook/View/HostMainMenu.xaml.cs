using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HostMainMenu : ContentPage
	{
		public HostMainMenu ()
		{
			InitializeComponent ();
		}
	}
}