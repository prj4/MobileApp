using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class HostViewViewModel : INotifyPropertyChanged
    {

        public INavigation Navigation;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public HostViewViewModel()
        {
        }


        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get { return _LoginCommand ?? (_LoginCommand = new DelegateCommand(Login_Execute)); }
        }

        private void Login_Execute()
        {
            Navigation.PushAsync(new HostLogin());
        }

        private ICommand _createUserCommand;
        public ICommand CreateUserCommand
        {
            get { return _createUserCommand ?? (_createUserCommand = new DelegateCommand(CreateUser_Execute)); }
        }

        private void CreateUser_Execute()
        {
            Navigation.PushAsync(new NewHost(new NewHostViewModel()));
        }



    }
}
