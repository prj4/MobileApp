using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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

        public ICommand DeleteEventCommand =>
            _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand(DeleteEvent_Execute));

        public ICommand AddEventCommand =>
            _addEventCommand ?? (_addEventCommand = new DelegateCommand(AddEvent_Execute));

        public ICommand LogoutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(Logout_Execute));


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DeleteEvent_Execute()
        {
            Events.Remove(SelectedEvent);
            TestText = _selectedEvent.Name;

            NotifyPropertyChanged("Events");
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


        #region Event liste

        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public ObservableCollection<Event> Events
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

        private Event _selectedEvent;

        public Event SelectedEvent
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