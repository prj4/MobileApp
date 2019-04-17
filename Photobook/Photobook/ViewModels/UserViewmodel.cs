using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Helpers;
using Photobook.Models;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class UserViewmodel: INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private ObservableCollection<Host> _hosts;
        public ObservableCollection<Host> Hosts
        {
            get { return _hosts; }
            set
            {
                _hosts = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<string> _tekst;
        public ObservableCollection<string> tekst
        {
            get { return _tekst; }
            set
            {
                _tekst = value;
                NotifyPropertyChanged();
            }
        }

        public UserViewmodel()
        {
            AddCommand = new Command((param) =>
            {
                tekst.Add("Ny tekst streng!!!");
                //Users.Add(new User() { ImageUrl = "ccc", Date = DateTime.Now, Key = 12, Username = "Hans peter" });
                //NotifyPropertyChanged();
            });
        }


        public ICommand AddCommand { get; set; }





    }
}
