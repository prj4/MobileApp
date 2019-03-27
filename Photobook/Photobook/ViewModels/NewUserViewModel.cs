using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Transactions;
using Prism.Commands;
using Prism.Common;
using System.Windows.Input;
using Photobook.Models;
using System.Runtime.CompilerServices;
using Photobook.View;
using Xamarin.Forms;


namespace Photobook.ViewModels
{
    public class NewUserViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        private IUserServerCommunicator Com;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewUserViewModel()
        {
            user = new User();
            Com = new UserServerCommunicator();
            SuccesTxt = "";
        }

        private User user;

        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged(); ((Command)NewUserCommand).ChangeCanExecute(); }
        }


        private string _passwordValidation;
        public string PasswordValidation
        {
            get { return _passwordValidation; }
            set 
            {   
                _passwordValidation = value;
                NotifyPropertyChanged();
               
            }
        }

        private string _SuccesTxt = "";
        public string SuccesTxt
        {
            get { return _SuccesTxt; }
            set { _SuccesTxt = value; NotifyPropertyChanged(); }
        }

      
        ICommand _newUserCommand;
        public ICommand NewUserCommand
        {
            get { return _newUserCommand ?? (_newUserCommand = new DelegateCommand(AddNewUser_Execute)); }
        }

        private void AddNewUser_Execute()
        {
            
            try
            {

                if (User.Password == PasswordValidation)
                {
                    SuccesTxt = "";
                }
                else
                {
                    SuccesTxt = "Check at dine passwords stemmer overens";
                }

                User.Validate();



                // Vi skla her tjekke, at hvis det er rigtigt, sendes der en anmodning til server
                // Om at oprette en ny bruger
                // Tjek om bruger indsættes == succes
                // Hvis brugeren er indsat: 
                // Gå videre til næste view med brugerens info.
                // Så vi får brugerens info her: 

                User newUser = new User();
                newUser.Email = "Troelsbleicken@remoulade.dk";
                newUser.Password = "123";
                newUser.Username = "Troels Bleicken";

                // Giv den nye user som input parameter og vis info.
                Navigation.PushAsync(new HostMainMenu());

            }
            catch(Exception e)
            {
                SuccesTxt = e.Message;
            }
            
        }

 


    }
}
