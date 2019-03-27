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

namespace Photobook.ViewModels
{
    public class NewUserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            set { _passwordValidation = value; NotifyPropertyChanged(); }
        }


        ICommand _newUserCommand;
        public ICommand NewUserCommand
        {
            get { return _newUserCommand ?? (_newUserCommand = new DelegateCommand(AddNewUser_Execute, AddNewUser_CanExecute)); }
        }

        private void AddNewUser_Execute()
        {
            // Gå videre til næste view her 
            try
            {

            }
            catch()
            {

            }
                
        }

        private bool AddNewUser_CanExecute()
        {
            if (User.Password == PasswordValidation)
                return true;
            return false;
        }

    }
}
