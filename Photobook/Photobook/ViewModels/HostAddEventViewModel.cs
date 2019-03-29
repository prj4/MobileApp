using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Photobook.Models;
using Xamarin.Forms;
using Prism.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;

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

        private NewEvent _newEvent;
        private User _user;
        private NewEvent _event;




        public HostAddEventViewModel(User user, ref NewEvent newEvent)
        {
            _user = user;
            _newEvent = newEvent;
        }

        public DateTime MinDate
        {
            get { return DateTime.Now; }
        }

        public NewEvent NewEvent
        {
            get { return _newEvent; }
            set { _newEvent = value; NotifyPropertyChanged(); }
        }

        private ICommand _createEventCommand;
        public ICommand CreateEventCommand
        {
            get { return _createEventCommand ?? (_createEventCommand = new DelegateCommand(CreateEvent_Execute)); }
        }

        private void CreateEvent_Execute()
        {


            // Når der trykkes "Opret event knappen"
            // Her skal data fra NewEvent.StartDate; NewEvent.EndDate; NewEvent.EventName
            // Og klassen "User" sendes til serveren, for at oprette event til den pågældende bruger, med Event info
            // Her kunne vi evt. bruge "SMS" eller "EMAIL" funktionen Poul snakkede om i essentials
            // Så når der trykkes på knappen, laves der en PIN kode og sendes også en sms til brugere med denne PIN

            Navigation.PopAsync();
        }

    }
}
