using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class UserImagesView : ContentPage
    {
        public UserImagesView()
        {
            InitializeComponent();
            BindingContext = new UpdateItemsGroupedPageViewmodel();
        }
    }
}