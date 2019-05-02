using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class HostMainMenuViewModel : INotifyPropertyChanged
    {
        private ICommand _addEventCommand;


        private ICommand _deleteEventCommand;

        private Host _host;

        private ICommand _logOutCommand;
        public INavigation Navigation;

        public HostMainMenuViewModel(Host host)
        {
            _selectedEvent = null;

            if (host != null)
            {
                _host = host;
            }
            else
            {
                _host = new Host();
                _host.Name = "Troels Bleicken";
            }
            _events = new ObservableCollection<EventModel>();
        }

        public HostMainMenuViewModel(ReturnHostModel hostModel)
        {
            
            
            _selectedEvent = null;

            if (hostModel != null)
            {
                _host = new Host(hostModel);
            }
            else
            {
                _host = new Host();
                _host.Name = "Troels Bleicken";
            }
            _events = new ObservableCollection<EventModel>();
        }

        public HostMainMenuViewModel(ReturnHostModel hostModel, List<EventModel> events)
        {
            _selectedEvent = null;

            if (hostModel != null)
            {
                _host = new Host(hostModel);
            }
            else
            {
                _host = new Host();
                _host.Name = "Troels Bleicken";
            }
            _events = new ObservableCollection<EventModel>(events);
        }

        public Host Host
        {
            get => _host;
            set
            {
                _host = value;
                NotifyPropertyChanged();
            }
        }

        public string Gesture => $"Hej {Host.Name}!";

        public ICommand AddEventCommand =>
            _addEventCommand ?? (_addEventCommand = new DelegateCommand(AddEvent_Execute));

        public ICommand LogoutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(Logout_Execute));


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        public ICommand DeleteEventCommand
        {
            get
            {
                return _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand<EventModel>(DeleteEvent_Execute));
            }
        }
      


        private async void DeleteEvent_Execute(EventModel _event)
        {
            IServerCommunicator com = new ServerCommunicator();

            if(await com.DeleteFromServer(_event, DataType.DeleteEvent))
            {
                Events.Remove(_event);

                NotifyPropertyChanged("Events");
            }
        }


        private void AddEvent_Execute()
        {
            _selectedEvent = null;
            Navigation.PushModalAsync(new HostAddEvent(Host, Events));
        }

        private void Logout_Execute()
        {
            var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new StartUpView(), Navigation.NavigationStack.First());
                Navigation.PopToRootAsync();
            }
        }


        #region EventModel liste

        private ObservableCollection<EventModel> _events;

        public ObservableCollection<EventModel> Events
        {
            get => _events;
            set
            {
                _events = value;
                NotifyPropertyChanged();
            }
        }

        public string EventList
        {
            get
            {
                if (Events.Count > 0)
                {
                    NotifyPropertyChanged();
                    return "Dine events";
                }

                NotifyPropertyChanged();
                return "Du har ikke nogle events endnu - opret et event";
            }
        }

        private EventModel _selectedEvent;

        public EventModel SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                NotifyPropertyChanged();


                // Skriv til server om at få info om dette event
                TestText = _selectedEvent.Name;
                Navigation.PushAsync(new ShowEvent(_selectedEvent, true));
            }
        }

        private string _testText;

        public string TestText
        {
            get => _testText;
            set
            {
                _testText = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}