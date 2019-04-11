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

namespace Photobook.ViewModels
{
    public class GuestLoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public List<GuestUser> LogIns { get; set; }

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
            LogIns = SettingsManager.GetAllActiveUsers();
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
            return (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Pin) && user.Pin.Length == 4);
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

           if (await Com.SendDataReturnIsValid(GuestUser, DataType.User))
           {
               var message = Data.LatestMessage;
               var parser = FromJSONFactory.Generate(ServerData.Event);

               ServerEvent info = (ServerEvent)await parser.DeserialisedData(message);

               SettingsManager.SaveInstance(GuestUser.UserName, Data.LatestReceivedCookies);

               var rootPage = Navigation.NavigationStack.FirstOrDefault();
               LogIns.Add(GuestUser);
               if (rootPage != null)
               {
                   // Det event brugeren er tilknyttet skal her hentes ned fra serveren, og gives som input parameter
                   // Det event brugeren er tilknyttet skal laves om til et NewEvent objekt og gives med som parameter. 


                   var EventFromServer = new NewEvent();
                   EventFromServer.Name = info.name;
                   EventFromServer.StartDate = info.Event.startDate;
                   EventFromServer.EndDate = info.Event.endDate;
                   EventFromServer.Description = info.Event.description;

                   Navigation.InsertPageBefore(new Event(EventFromServer, false),
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
