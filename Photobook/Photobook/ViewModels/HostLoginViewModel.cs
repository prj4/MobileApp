﻿using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class HostLoginViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        private IMemoryManager _memoryManager;

        public Host Host { get; set; } = new Host();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public HostLoginViewModel(IMemoryManager memoryManager = null)
        {
            _memoryManager = memoryManager ?? MemoryManager.GetInstance();
        }

        #region Commands

        private ICommand _LoginCommand;

        public ICommand LoginCommand => _LoginCommand ?? (_LoginCommand = new DelegateCommand(Login_Execute));

        private async void Login_Execute()
        {
            // Denne metode skal der hentes bruger data fra serveren
            // Den bruger data der hentes, skal sendes videre til næste view. Som er HostMenu.
            // Brugeren hente ned og bruger data tilføres et bruger objekt. 

            IServerDataHandler handler = new ServerDataHandler();
            IServerCommunicator Com = new ServerCommunicator(handler);

            if (await Com.SendDataReturnIsValid(Host, DataType.Host))
            {
                IFromJSONParser Parser = new FromJsonParser();

                var ServerHost = await Parser.DeserializedData<ReturnHostModel>(handler.LatestMessage);

                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    var page = (ServerHost.Events != null)
                        ? new HostMainMenu(ServerHost, ServerHost.Events.ToList())
                        : new HostMainMenu(ServerHost);

                    _memoryManager.SaveCookie(handler.LatestReceivedCookies, ServerHost.Name);

                    Navigation.InsertPageBefore(page, Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
        }

        #endregion
    }
}