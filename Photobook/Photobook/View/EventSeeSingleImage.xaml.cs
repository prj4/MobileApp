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
	public partial class EventSeeSingleImage : ContentPage
	{
		public EventSeeSingleImage (string imgUrl)
		{
            var vm = new EventSeeSingleImageViewModel(imgUrl);
            BindingContext = vm;
            InitializeComponent ();
		}
	}
}