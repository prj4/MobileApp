﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class GuestLoginViewModel : INotifyPropertyChanged
    {
        private Guest _guest = new Guest();

        private ICommand _guestLoginCommand;
        private string _loginInfo;
 

        private bool enableButton = true;
        private EventModel eventFromServer;
        public INavigation Navigation;

        public GuestLoginViewModel()
        {
            Guest = new Guest();
            eventFromServer = new EventModel();

            InitializeGuests();
        }

        public bool EnableButton
        {
            get => enableButton;
            set
            {
                enableButton = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<GuestAtEvent> ActiveGuests { get; set; }


        private GuestAtEvent current;
        public GuestAtEvent Current
        {
            get => current;
            set
            {
                current = value;
                _guest = current.GuestInfo;
            }
        }

        public Guest Guest
        {
            get => _guest;
            set
            {
                _guest = value;
                NotifyPropertyChanged();
            }
        }

        public string LoginInfo
        {
            get => _loginInfo;
            set
            {
                _loginInfo = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand GuestLoginCommand =>
            _guestLoginCommand ?? (_guestLoginCommand = new DelegateCommand(Login_Execute));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void InitializeGuests()
        {
            var list = await SettingsManager.GetAllActiveUsers();
            ActiveGuests = new ObservableCollection<GuestAtEvent>(list);
            NotifyPropertyChanged();
        }

        private bool AreDetailsValid(Guest guest)
        {
            return !string.IsNullOrWhiteSpace(guest.Username) && !string.IsNullOrWhiteSpace(guest.Pin);
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

                eventFromServer = await parser.DeserializedData<EventModel>(message);


                SettingsManager.SaveCookie(Data.LatestReceivedCookies, _guest.Username);
                current = new GuestAtEvent
                {
                    EventInfo = eventFromServer,
                    GuestInfo = _guest
                };
                ActiveGuests.Add(current);
                SettingsManager.SaveActiveGuestList(ActiveGuests.ToList());

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