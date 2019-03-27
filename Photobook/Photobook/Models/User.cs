using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Photobook.Models
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /*

        public User(string url, string name, DateTime date)
        {
            ImageUrl = url;
            Username = name;
            Date = date;

        }
        */

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged(); }

        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }

        }


        private string _imageUrl;
        public string ImageUrl 
        { 
            get { return _imageUrl; }
            set { _imageUrl = value; NotifyPropertyChanged(); }

        }

        private ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get { return _images; }
            set { _images = value; NotifyPropertyChanged(); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; NotifyPropertyChanged(); }
        }

        private DateTime _date;
        public DateTime Date 
        { 
            get { return _date; }
            set { _date = value; NotifyPropertyChanged(); }
        }




    }
}
