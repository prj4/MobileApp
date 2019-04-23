using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.Models.ServerDataClasses;
using Prism.Commands;
using Xamarin.Forms;
using Photobook.View;
using Xamarin.Forms.Internals;

namespace Photobook.ViewModels
{
    public class GuestLoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public List<Guest> LogIns { get; set; }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;
        private Guest _guest = new Guest();
        private string _loginInfo;


        public GuestLoginViewModel()
        {
            Guest = new Guest();
            LogIns = SettingsManager.GetAllActiveUsers();

            foreach (var log in LogIns)
            {
                LoginInfo += log.Username;
            }

            
        }

        public Guest Guest
        {
            get { return _guest; }
            set { _guest = value; NotifyPropertyChanged(); }
        }

        public string LoginInfo
        {
            get { return _loginInfo; }
            set { _loginInfo = value; NotifyPropertyChanged(); }
        }

        bool AreDetailsValid(Guest guest)
        {
            return (!string.IsNullOrWhiteSpace(guest.Username) && !string.IsNullOrWhiteSpace(guest.Pin) && guest.Pin.Length == 4);
        }

        private ICommand _guestLoginCommand;
        public ICommand GuestLoginCommand
        {
            get { return _guestLoginCommand ?? (_guestLoginCommand = new DelegateCommand(Login_Execute)); }
        }

        private async void Login_Execute()
        {
           IServerDataHandler Data = new ServerDataHandler();
           IServerCommunicator Com = new ServerCommunicator(Data);

           if (await Com.SendDataReturnIsValid(_guest, DataType.Guest))
           {
               var message = Data.LatestMessage;
               IFromJSONParser parser = new FromJsonParser();

               Event eventFromServer = await parser.DeserializedData<Event>(message);
                
               

               SettingsManager.SaveInstance(Guest.Username, Data.LatestReceivedCookies);

               var rootPage = Navigation.NavigationStack.FirstOrDefault();
               LogIns.Add(Guest);
               if (rootPage != null)
               {
                   // Det event brugeren er tilknyttet skal her hentes ned fra serveren, og gives som input parameter
                   // Det event brugeren er tilknyttet skal laves om til et NewEvent objekt og gives med som parameter. 
;

                   Navigation.InsertPageBefore(new ShowEvent(eventFromServer, false),
                       Navigation.NavigationStack.First());
                   Navigation.PopToRootAsync();
               }
           }
           else
           {
               LoginInfo = "Error logging on. Is the pin correct?";
           }
        }

    }
}
