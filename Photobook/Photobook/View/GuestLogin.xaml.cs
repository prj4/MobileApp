using System;
using System.Collections.Generic;
using Photobook.Models;

using Xamarin.Forms;

namespace Photobook.View
{
    public partial class GuestLogin : ContentPage
    {
        public GuestLogin()
        {
            InitializeComponent();
        }

        void LogInd_Clicked(object sender, System.EventArgs e)
        {
            var GuestUser = new GuestUser()
            {
                Username = usernameEntry.Text,
                PIN = passwordEntry.Text
            };

            IServerCommunicator Com = new ServerCommunicator();

            var status = Com.SendGuestLogon(GuestUser);
            if (status != null)
            {
                if (status.Result)
                {
                    //Kør noget kode her
                }
                else
                {
                    //"Sign up failed"
                }
            }

            // Sign up logic
            /*
            var signUpSucceeded = AreDetailsValid(GuestUser);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new MainPageCS(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.Text = "Sign up failed";
            }
            */
        }

        bool AreDetailsValid(GuestUser user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.PIN));
        }


    }
}
