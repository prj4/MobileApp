using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.Models.ServerClasses;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class StartUpViewViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        public StartUpViewViewModel(INavigation nav)
        {
            Navigation = nav;
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Commands

        private ICommand _guestLogin;

        public ICommand GuestLoginCommand => _guestLogin ?? (_guestLogin = new DelegateCommand(GuestLogin_Execute));

        private async void GuestLogin_Execute()
        {
            await Navigation.PushAsync(new GuestLogin());
        }

        private ICommand _hostView;

        public ICommand HostViewCommand => _hostView ?? (_hostView = new DelegateCommand(HostView_Execute));

        private async void HostView_Execute()
        {
            await Navigation.PushAsync(new HostView());
        }

        #endregion
    }
}
