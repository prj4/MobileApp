using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;
using Photobook.View;
using System.Collections.ObjectModel;
using Photobook.Models;

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
            if (user != null)
                _user = user;
            else
            {
                _user = new User();
                _user.Username = "Troels Bleicken";
            }

            LoadDummydata();



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

        public void LoadDummydata()
        {

            Events.Add(new NewEvent(DateTime.Now, DateTime.Parse("2019-05-19"), "Barnedåb"));
            Events.Add(new NewEvent(DateTime.Now, DateTime.Parse("2019-05-29"), "Fest"));
        }


        public string EventList
        {
            get
            {
                if (Events.Count > 0)
                {
                    return $"Dine events";
                }
                else
                {
                    return $"Du har ikke nogle events endnu - opret et event";
                }

            }
        }



        #endregion













        private ICommand _seeEventsCommand;
        public ICommand SeeEventsCommand
        {
            get { return _seeEventsCommand ?? (_seeEventsCommand = new DelegateCommand(SeeEvents_Execute)); }
        }

        private void SeeEvents_Execute()
        {
            // Navigation.PushAsync();
        }

        private ICommand _addEventCommand;
        public ICommand AddEventCommand
        {
            get { return _addEventCommand ?? (_addEventCommand = new DelegateCommand(AddEvent_Execute)); }
        }

        private void AddEvent_Execute()
        {
            var newEvent = new NewEvent();
            Events.Add(newEvent);
            Navigation.PushAsync(new HostAddEvent(User, ref newEvent));

        }

    }
}
