using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Photobook.Models.ServerClasses;
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

        private bool enableButton = true;

        public bool EnableButton
        {
            get { return enableButton; }
            set { enableButton = value;NotifyPropertyChanged(); }
        }
        public INavigation Navigation;
        private Guest _guest = new Guest();
        private string _loginInfo;
        private GuestAtEvent current;
        private Event eventFromServer;
        private List<GuestAtEvent> activeGuests;
        public List<GuestAtEvent> ActiveGuests
        {
            get { return activeGuests; }
            set { activeGuests = value; }
        }

        public GuestAtEvent Current
        {
            get { return current;}
            set
            {
                current = value;
                _guest = current.GuestInfo;
            }
        }

        public GuestLoginViewModel()
        {
            Guest = new Guest();
            eventFromServer = new Event();
            
            InitializeGuests();
        }

        private async void InitializeGuests()
        {
            activeGuests = await SettingsManager.GetAllActiveUsers();
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
            return (!string.IsNullOrWhiteSpace(guest.Username) && !string.IsNullOrWhiteSpace(guest.Pin));
        }

        private ICommand _guestLoginCommand;
        public ICommand GuestLoginCommand
        {
            get { return _guestLoginCommand ?? (_guestLoginCommand = new DelegateCommand(Login_Execute)); }
        }

        private async void Login_Execute()
        {
           EnableButton = false;
           IServerDataHandler Data = new ServerDataHandler();
           IServerErrorcodeHandler errroHandler = new GuestLoginErrorcodeHandler();
           IServerCommunicator Com = new ServerCommunicator(Data, errroHandler);

           if (await Com.SendDataReturnIsValid(_guest, DataType.Guest))
           {
               var message = Data.LatestMessage;
               IFromJSONParser parser = new FromJsonParser();

               eventFromServer = await parser.DeserializedData<Event>(message);
               

               SettingsManager.SaveCookie(Data.LatestReceivedCookies, _guest.Username);
               current = new GuestAtEvent
               {
                   EventInfo = eventFromServer,
                   GuestInfo = _guest
               };
               activeGuests.Add(current);
               SettingsManager.SaveActiveGuestList(activeGuests);

               var rootPage = Navigation.NavigationStack.FirstOrDefault();
               if (rootPage != null)
               {
                   // Det event brugeren er tilknyttet skal her hentes ned fra serveren, og gives som input parameter
                   // Det event brugeren er tilknyttet skal laves om til et NewEvent objekt og gives med som parameter. 

                   Navigation.InsertPageBefore(new ShowEvent(eventFromServer, _guest, false),
                       Navigation.NavigationStack.First());
                   Navigation.PopToRootAsync();
               }
           }
           else
           {
               LoginInfo = errroHandler.Message;
               EnableButton = true;
           }
        }

    }
}
