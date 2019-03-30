using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Prism.Commands;
using System.Windows.Input;

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


        public EventViewModel()
        {
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

        #endregion


    }
}
