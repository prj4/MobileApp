
using Photobook.Models;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class StartUpView : ContentPage
    {
        public StartUpView()
        {
            InitializeComponent();

#if DEBUG
            Button TestBtn = new Button();
            TestBtn.Text = "Troels' store testknap";
            MainStack.Children.Add(TestBtn);

            TestBtn.Clicked += async(sender, args) =>
            {
                var result = await DependencyService.Get<ICameraAPI>().TakePhotoReturnPath();
                
                IUserServerCommunicator Com = new UserServerCommunicator();

                if(result != "Null")
                    Com.UploadPhoto(result);

            };
#endif

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GuestLogin());
        }

        async void Host_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HostView());
        }

    }
}
