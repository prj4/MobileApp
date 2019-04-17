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
    public class UpdateItemsGroupedPageViewmodel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        static int insertId = 0;


        private ObservableCollection<Grouping<string, Guest>> _users;
        public ObservableCollection<Grouping<string, Guest>> Users
        {
            get { return _users; }
            set 
            {
                _users = value;
                NotifyPropertyChanged();
            }
        }

        public void ReloadData()
        {
            var exampleData = new ObservableCollection<Guest>();
            /*
            for (int grId = 0; grId < 5; grId++)
            {
                var howMany = new Random().Next(15, 20);

                for (int i = 0; i < howMany; i++)
                {
                    exampleData.Add(new User() { Username = string.Format("{0}:{1}", grId, i) });
                }
            }
            */
        }


        //    var sorted = exampleData
        //        .OrderBy(item => item.Username)
        //        .ThenBy(item => item.Username.Length)
        //        .GroupBy(item => item.Username)
        //        .Select(itemGroup => new Grouping<string, Host>(itemGroup.Key, itemGroup));

        //    Users = new ObservableCollection<Grouping<string, User>>(sorted);
        //}


        public ICommand ItemTappedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public UpdateItemsGroupedPageViewmodel()
        {
            ReloadData();

            ItemTappedCommand = new Command((param) =>
            {

                var user = LastTappedUser as Guest;
                if (user != null)
                    System.Diagnostics.Debug.WriteLine("Tapped {0}", user.Username);

            });

            AddCommand = new Command((param) =>
            {
                insertId++;
                Users[0].Insert(10, new Guest() { Username = string.Format("New {0}", insertId) });
            });

        }






        private object _lastTappedUser;

        public object LastTappedUser
        {
            get { return _lastTappedUser; }
            set
            {
                _lastTappedUser = value;
                NotifyPropertyChanged();
            }
        }






    }
}
