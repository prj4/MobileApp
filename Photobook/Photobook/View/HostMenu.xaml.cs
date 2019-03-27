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
	public partial class HostMenu : TabbedPage
	{
		public HostMenu ()
		{
			InitializeComponent ();

            this.Children.Add(new SeeImages());
            this.Children.Add(new SeeImages());
		}
	}
}