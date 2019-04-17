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
    public class HostLoginViewModel : INotifyPropertyChanged
    {
        public Host Host { get; set; } = new Host();
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

        private async void Login_Execute()
        {
            // Denne metode skal der hentes bruger data fra serveren
            // Den bruger data der hentes, skal sendes videre til næste view. Som er HostMenu.
            // Brugeren hente ned og bruger data tilføres et bruger objekt. 

            IServerDataHandler handler = new ServerDataHandler();
            IServerCommunicator Com = new ServerCommunicator(handler);

            if (await Com.SendDataReturnIsValid(Host, DataType.Host))
            {
                

                IFromJSONParser Parser = FromJSONFactory.Generate(ServerData.Host);

                var ServerHost = (ServerHostResponse)await Parser.DeserialisedData(handler.LatestMessage);

                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    Host updatedUser = new Host()
                    {
                        Email = ServerHost.email,
                        Username = ServerHost.name
                    };
                    SettingsManager.SaveInstance($"Cookie{updatedUser.Username}", handler.LatestReceivedCookies);
                    Navigation.InsertPageBefore(new HostMainMenu(updatedUser), Navigation.NavigationStack.First());
                    Navigation.PopToRootAsync();
                }
            }

            

        }

        #endregion

   


    }
}
