using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;
using Photobook.View;
using System.Collections.ObjectModel;
using Photobook.Models;
using System.Linq;

namespace Photobook.ViewModels
{
    public class HostMainMenuViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private User _user;
        public HostMainMenuViewModel(User user)
        {
            _selectedEvent = null;

            if (user != null)
                _user = user;
            else
            {
                _user = new User();
                _user.Username = "Troels Bleicken";
            }




        }

        public User User
        {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged(); }
        }

        public string Gesture
        {
            get
            {
                return $"Hej {User.Username}!";
            }
        }



        #region Event liste

        private ObservableCollection<NewEvent> _events = new ObservableCollection<NewEvent>();
        public ObservableCollection<NewEvent> Events
        {
            get { return _events; }
            set { _events = value; NotifyPropertyChanged(); }
        }

        public string EventList
        {
            get
            {
                if (Events.Count > 0)
                {
                    NotifyPropertyChanged();
                    return $"Dine events";
                }
                else
                {
                    NotifyPropertyChanged();
                    return $"Du har ikke nogle events endnu - opret et event";
                }


            }
        }

        private NewEvent _selectedEvent;
        public NewEvent SelectedEvent
        {
            get { return _selectedEvent; }
            set 
            { 
                _selectedEvent = value;
                NotifyPropertyChanged();


                // Skriv til server om at få info om dette event
                TestText = _selectedEvent.EventName;
                Navigation.PushAsync(new Event(_selectedEvent, true));
            }
        }

        private string _testText;
        public string TestText
        {
            get { return _testText; }
            set { _testText = value;  NotifyPropertyChanged(); }
        }


        #endregion


        private ICommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand(DeleteEvent_Execute)); }
        }

        private void DeleteEvent_Execute()
        {
            Events.Remove(SelectedEvent);
            TestText = _selectedEvent.EventName;

            NotifyPropertyChanged("Events");
        }


 

        private ICommand _addEventCommand;
        public ICommand AddEventCommand
        {
            get { return _addEventCommand ?? (_addEventCommand = new DelegateCommand(AddEvent_Execute)); }
        }

        
        private void AddEvent_Execute()
        {
            _selectedEvent = null;
            Navigation.PushModalAsync(new HostAddEvent(User, Events));

        }

        private ICommand _logOutCommand;
        public ICommand LogoutCommand
        {
            get { return _logOutCommand ?? (_logOutCommand = new DelegateCommand(Logout_Execute)); }
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


    }
}
