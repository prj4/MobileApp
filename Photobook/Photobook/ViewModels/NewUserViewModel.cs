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
            SuccesTxt = "dasdasdasdas";
        }

        private User user;

        public User User
        {
            get { return user; }
            set { user = value; NotifyPropertyChanged(); }
        }


        private string _passwordValidation;
        public string PasswordValidation
        {
            get { return _passwordValidation; }
            set { _passwordValidation = value; NotifyPropertyChanged();}
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
                User.Validate();
            }
            catch(Exception e)
            {
                SuccesTxt = e.Message;
            }
            
            Com.SendUserInformation(User);
            

            SuccesTxt = "";
        }

        public bool AddNewUser_CanExecute()
        {
            if (User.Password == PasswordValidation)
            {
                SuccesTxt = "";
                return true;
            }
                

            SuccesTxt = "Check at dine passwords stemmer overens";
            return false;
        }

    }
}
