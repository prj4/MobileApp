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


        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
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

            tekst = new ObservableCollection<string>();
            Users = new ObservableCollection<User>();

            Users.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans Petersen", Date = DateTime.Now });
            Users.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans Larsen", Date = DateTime.Now });
            Users.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans A", Date = DateTime.Now });
            Users.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans DS", Date = DateTime.Now });
            Users.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans AS", Date = DateTime.Now});


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
