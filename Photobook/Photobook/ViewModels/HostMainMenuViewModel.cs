using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;
using Photobook.View;

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
            Navigation.PushAsync(new HostAddEvent(User));
        }

    }
}
