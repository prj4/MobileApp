using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class NewHostViewModel : INotifyPropertyChanged
    {
        private ICommand _newUserCommand;


        private string _passwordValidation;

        private string _SuccesTxt = "";
        private readonly IServerCommunicator Com;

        private readonly IServerDataHandler dataHandler;
        private IMemoryManager _memoryManager;

        private Host host;
        private bool loggedIn;
        public INavigation Navigation;

        public NewHostViewModel(IMemoryManager memoryManager = null)
        {
            host = new Host();
            dataHandler = new ServerDataHandler();
            Com = new ServerCommunicator(dataHandler);
            SuccesTxt = "";
            _memoryManager = memoryManager ?? MemoryManager.GetInstance();
        }

        public Host Host
        {
            get => host;
            set
            {
                host = value;
                NotifyPropertyChanged();
                ((Command) NewUserCommand).ChangeCanExecute();
            }
        }

        public string PasswordValidation
        {
            get => _passwordValidation;
            set
            {
                _passwordValidation = value;
                NotifyPropertyChanged();
            }
        }

        public string SuccesTxt
        {
            get => _SuccesTxt;
            set
            {
                _SuccesTxt = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NewUserCommand =>
            _newUserCommand ?? (_newUserCommand = new DelegateCommand(AddNewUser_Execute));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void AddNewUser_Execute()
        {
            loggedIn = false;
            if (loggedIn)
            {
                await Navigation.PushAsync(new HostMainMenu(Host));
            }
            else
            {
                if (Host.Password == PasswordValidation)
                {
                    SuccesTxt = "";
                    try
                    {
                        Host.Validate();
                    }
                    catch (Exception e)
                    {
                        SuccesTxt = e.Message;
                        return;
                    }

                    if (await Com.SendDataReturnIsValid(Host, DataType.NewUser))
                    {
                        _memoryManager.SaveCookie(dataHandler.LatestReceivedCookies, host.Name);

                        var rootPage = Navigation.NavigationStack.FirstOrDefault();
                        if (rootPage != null)
                        {
                            Navigation.InsertPageBefore(new HostMainMenu(Host), Navigation.NavigationStack.First());
                            await Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await Navigation.PushAsync(new HostMainMenu(Host));
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