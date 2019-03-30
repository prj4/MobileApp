using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Prism.Commands;
using System.Windows.Input;
using Photobook.Models;
using System.Linq;
using Photobook.View;

namespace Photobook.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;
        private NewEvent _event;
        private string _pin;
        private string _date;
        private string _startDate;
        private string _endDate;
        private bool _showTopBar;
        private bool _showLogoutBtn;

        public EventViewModel(NewEvent newEvent, bool ShowNavBar)
        {
            _event = newEvent;
            ShowTopBar = ShowNavBar;

            if (ShowNavBar)
                ShowLogoutBtn = false;
            else
                ShowLogoutBtn = true;

            PIN = "1234";
        }

        public bool ShowTopBar
        {
            get { return _showTopBar; }
            set { _showTopBar = value; NotifyPropertyChanged(); }
        }

        public bool ShowLogoutBtn
        {
            get { return _showLogoutBtn; }
            set { _showLogoutBtn = value; NotifyPropertyChanged(); }
        }

        public string EventName
        {
            get { return _event.EventName; }
            set { _event.EventName = value; NotifyPropertyChanged(); }
        }



        public string Date 
        {
            get { return $"Dato: {_event.StartDate.Date.ToString()} - {_event.EndDate.Date.ToString()}"; }
            private set { }
        }



        public string PIN
        {
            get { return $"PIN: {_pin}"; }
            set { _pin = value; NotifyPropertyChanged(); }
        }

        #region Commands


        private ICommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand(DeleteEvent_Execute)); }
        }

        private void DeleteEvent_Execute()
        {

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


        #endregion


    }
}
