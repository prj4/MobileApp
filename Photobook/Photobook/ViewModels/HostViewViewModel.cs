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
        private ICommand _createUserCommand;


        private ICommand _LoginCommand;

        public INavigation Navigation;


        public ICommand LoginCommand => _LoginCommand ?? (_LoginCommand = new DelegateCommand(Login_Execute));

        public ICommand CreateUserCommand =>
            _createUserCommand ?? (_createUserCommand = new DelegateCommand(CreateUser_Execute));


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Login_Execute()
        {
            await Navigation.PushAsync(new HostLogin());
        }

        private void CreateUser_Execute()
        {
            Navigation.PushAsync(new NewHost(new NewHostViewModel()));
        }
    }
}