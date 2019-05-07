using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class HostAddEventViewModel : INotifyPropertyChanged
    {
        private ICommand _createEventCommand;
        private DateTime _endDate = new DateTime();
        private TimeSpan _endTime;
        private readonly ObservableCollection<EventModel> _events;
        private readonly Host _host;


        private bool _isErrorMessageEnabled;

        private EventModel _newEvent = new EventModel();

        private ICommand _regretCommand;
        private DateTime _startDate = new DateTime();
        private TimeSpan _startTime;

        public INavigation Navigation;

        public HostAddEventViewModel(Host host, ObservableCollection<EventModel> events)
        {
            _host = host;
            _events = events;
            isErrorMessageEnabled = false;
        }

        public bool isErrorMessageEnabled
        {
            get => _isErrorMessageEnabled;
            set
            {
                _isErrorMessageEnabled = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                NotifyPropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                NotifyPropertyChanged();
            }
        }


        public DateTime MinDate => DateTime.Today.Date;

        public EventModel NewEvent
        {
            get => _newEvent;
            set
            {
                _newEvent = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get => _newEvent.Description;
            set
            {
                _newEvent.Description = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => NewEvent.StartDate;
            set
            {
                NewEvent.StartDate = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => NewEvent.EndDate;
            set
            {
                NewEvent.EndDate = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand CreateEventCommand =>
            _createEventCommand ?? (_createEventCommand = new DelegateCommand(CreateEvent_Execute));

        public ICommand RegretCommand => _regretCommand ?? (_regretCommand = new DelegateCommand(Regret_Execute));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void CreateEvent_Execute()
        {
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
                StartDate = StartDate + StartTime;
                EndDate = EndDate + EndTime;
                isErrorMessageEnabled = false;
            }

            var cookie = MemoryManager.CurrentCookies;

            IServerDataHandler dataHandler = new ServerDataHandler();
            IServerCommunicator Com = new ServerCommunicator(dataHandler);
            Com.AddCookies(cookie);

            if (await Com.SendDataReturnIsValid(_newEvent, DataType.NewEvent))
            {
                var response = dataHandler.LatestMessage;
                IFromJSONParser parser = new FromJsonParser();

                var pin = await parser.DeserializedData<Dictionary<string, string>>(response);

                NewEvent.Pin = pin["pin"];
                _events.Add(NewEvent);
                Navigation.PopModalAsync();
            }
        }

        private void Regret_Execute()
        {
            Navigation.PopModalAsync();
        }
    }
}