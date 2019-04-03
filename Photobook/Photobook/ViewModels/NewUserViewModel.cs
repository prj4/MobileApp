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
using System.Linq;

namespace Photobook.ViewModels
{
    public class NewUserViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        private IServerCommunicator Com;
        private bool loggedIn = false;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewUserViewModel()
        {
            user = new User();
            Com = new ServerCommunicator();
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

        private async void AddNewUser_Execute()
        {
            loggedIn = false;
            if(loggedIn)
            {
                Navigation.PushAsync(new HostMainMenu(User));
            }
            else
            {
                if (User.Password == PasswordValidation)
                {
                    SuccesTxt = "";
                    try
                    {
                        User.Validate();
                    }
                    catch (Exception e)
                    {
                        SuccesTxt = e.Message;
                        return;
                    }

                    if (await Com.SendNewUserInfoReturnIsValid(User))
                    {
                        var rootPage = Navigation.NavigationStack.FirstOrDefault();
                        if (rootPage != null)
                        {
                            Navigation.InsertPageBefore(new HostMainMenu(User), Navigation.NavigationStack.First());
                            Navigation.PopToRootAsync();
                        }
                        else
                        {
                            Navigation.PushAsync(new HostMainMenu(User));
                        }
                    }
                    else
                    {
                        SuccesTxt = "Fejl ved login";
                    }
                    
                }
                else
                {
                    SuccesTxt = "Check om at det er at dine passwords stemmer overens";
                }

                

                // Vi skla her tjekke, at hvis det er rigtigt, sendes der en anmodning til server
                // Om at oprette en ny bruger
                // Tjek om bruger indsættes == succes
                // Hvis brugeren er indsat: 
                // Gå videre til næste view med brugerens info.
                // Så vi får brugerens info her: 


                // Giv den nye user som input parameter og vis info.

                
            }
            
        }

    }
}
