using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Prism.Commands;
using Xamarin.Forms;
using Photobook.View;

namespace Photobook.ViewModels
{
    public class GuestLoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;
        private GuestUser _guestUser;
        private string _loginInfo;


        public GuestLoginViewModel()
        {
            GuestUser = new GuestUser();
        }

        public GuestUser GuestUser
        {
            get { return _guestUser; }
            set { _guestUser = value; NotifyPropertyChanged(); }
        }

        public string LoginInfo
        {
            get { return _loginInfo; }
            set { _loginInfo = value; NotifyPropertyChanged(); }
        }

        bool AreDetailsValid(GuestUser user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.PIN) && user.PIN.Length == 4);
        }

        private ICommand _guestLoginCommand;
        public ICommand GuestLoginCommand
        {
            get { return _guestLoginCommand ?? (_guestLoginCommand = new DelegateCommand(Login_Execute)); }
        }

        private void Login_Execute()
        {
            var signUpSucceeded = AreDetailsValid(GuestUser);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    // Det event brugeren er tilknyttet skal her hentes ned fra serveren, og gives som input parameter
                    // Det event brugeren er tilknyttet skal laves om til et NewEvent objekt og gives med som parameter. 


                    var EventFromServer = new NewEvent();
                    EventFromServer.EventName = "Bryllup ved Jane og Lorte Lars";
                    EventFromServer.StartDate = DateTime.Parse("08/18/2019");
                    EventFromServer.EndDate = DateTime.Parse("08/22/2019");

                    Navigation.InsertPageBefore(new Event(EventFromServer, false), Navigation.NavigationStack.First());
                    Navigation.PopToRootAsync();
                }
            }
            else
            {
                LoginInfo = "Skriv dit fulde navn, og angiv en PIN kode på 4 cifre";
            }
        }

    }
}
