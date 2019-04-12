﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Photobook.Models;
using Xamarin.Forms;
using Prism.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Photobook.ViewModels
{
    public class HostAddEventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;

        private NewEvent _newEvent = new NewEvent();
        private User _user;
        private ObservableCollection<NewEvent> _events;
        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();


        private bool _isErrorMessageEnabled;
        public bool isErrorMessageEnabled
        {
            get { return _isErrorMessageEnabled; }
            set { _isErrorMessageEnabled = value;  NotifyPropertyChanged(); }
        }

        public HostAddEventViewModel(User user, ObservableCollection<NewEvent> events)
        {
            _user = user;
            _events = events;
            isErrorMessageEnabled = false;
        }

        public DateTime MinDate
        {
            get { return DateTime.Today.Date; }
        }

        public NewEvent NewEvent
        {
            get { return _newEvent; }
            set { _newEvent = value; NotifyPropertyChanged(); }

        }

        public string Description
        {
            get { return _newEvent.Description; }
            set
            {
                _newEvent.Description = value; 
                NotifyPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return NewEvent.StartDate.Date; }
            set 
            {
                if (NewEvent.StartDate == null)
                    NewEvent.StartDate = DateTime.Today;
                else
                    NewEvent.StartDate = value; NotifyPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get { return NewEvent.EndDate.Date; }
            set 
            {
                if (NewEvent.EndDate == null)
                    NewEvent.EndDate = DateTime.Today;
                else
                    NewEvent.EndDate = value; NotifyPropertyChanged();

            }
        }



        private ICommand _createEventCommand;
        public ICommand CreateEventCommand
        {
            get { return _createEventCommand ?? (_createEventCommand = new DelegateCommand(CreateEvent_Execute)); }
        }

        private async void CreateEvent_Execute()
        {

            IServerCommunicator Com  = new ServerCommunicator();

            if (await Com.SendDataReturnIsValid(_newEvent, DataType.NewEvent))
            {
                Debug.WriteLine("Succes", "NEW_EVENT");
            }
            else
            {
                Debug.WriteLine("Failure", "NEW_EVENT");
            }        
            

            // Når der trykkes "Opret event knappen"
            // Her skal data fra NewEvent.StartDate; NewEvent.EndDate; NewEvent.EventName
            // Og klassen "User" sendes til serveren, for at oprette event til den pågældende bruger, med Event info
            // Her kunne vi evt. bruge "SMS" eller "EMAIL" funktionen Poul snakkede om i essentials
            // Så når der trykkes på knappen, laves der en PIN kode og sendes også en sms til brugere med denne PIN

            if (_newEvent.Description.Length > 50)
            {
                isErrorMessageEnabled = true;
            }
            else
            {
                isErrorMessageEnabled = false;
                _events.Add(NewEvent);
                Navigation.PopModalAsync();
            }

        }

        private ICommand _regretCommand;
        public ICommand RegretCommand
        {
            get { return _regretCommand ?? (_regretCommand = new DelegateCommand(Regret_Execute)); }
        }

        private void Regret_Execute()
        {

            Navigation.PopModalAsync();
        }

    }
}
