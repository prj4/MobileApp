using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class HostLoginViewModel : INotifyPropertyChanged
    {
        private User user;
        public string Email
        {
            get { return user.Email;}
            set { user.Email = value; }
        }

        public string Username
        {
            get { return user.Username; }
            set { user.Username = value; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;


        public HostLoginViewModel()
        {
        }


        #region Commands


        private ICommand _LoginCommand;
        public ICommand LoginCommand
        {
            get { return _LoginCommand ?? (_LoginCommand = new DelegateCommand(Login_Execute)); }
        }

        private void Login_Execute()
        {
            // Denne metode skal der hentes bruger data fra serveren
            // Den bruger data der hentes, skal sendes videre til næste view. Som er HostMenu.
            // Brugeren hente ned og bruger data tilføres et bruger objekt. 

            

            var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new HostMainMenu(user), Navigation.NavigationStack.First());
                Navigation.PopToRootAsync();
            }

        }

        #endregion

   


    }
}
