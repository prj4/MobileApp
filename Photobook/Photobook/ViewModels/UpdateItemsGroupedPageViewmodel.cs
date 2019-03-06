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


        private ObservableCollection<Grouping<string, User>> _users;
        public ObservableCollection<Grouping<string, User>> Users
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
            var exampleData = new ObservableCollection<User>();
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

            User user1 = new User();
            user1.Username = "Hans Larsen";
            user1.Images = new ObservableCollection<string>();
            user1.Images.Add("url1");
            user1.Images.Add("url22");
            user1.Images.Add("url3");
            user1.Images.Add("url4");
            user1.Images.Add("url5");
            exampleData.Add(user1);


            exampleData.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans Petersen", Date = DateTime.Now });
            exampleData.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Hans Larsen", Date = DateTime.Now });
            exampleData.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Tove A", Date = DateTime.Now });
            exampleData.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Lars DS", Date = DateTime.Now });
            exampleData.Add(new User() { ImageUrl = "https://images.pexels.com/photos/1957155/pexels-photo-1957155.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260", Username = "Åse AS", Date = DateTime.Now });
           
            var sorted = exampleData
                .OrderBy(item => item.Username)
                .ThenBy(item => item.Username.Length)
                .GroupBy(item => item.Username)
                .Select(itemGroup => new Grouping<string, User>(itemGroup.Key, itemGroup));

            Users = new ObservableCollection<Grouping<string, User>>(sorted);
        }


        public ICommand ItemTappedCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public UpdateItemsGroupedPageViewmodel()
        {
            ReloadData();

            ItemTappedCommand = new Command((param) =>
            {

                var user = LastTappedUser as User;
                if (user != null)
                    System.Diagnostics.Debug.WriteLine("Tapped {0}", user.Username);

            });

            AddCommand = new Command((param) =>
            {
                insertId++;
                Users[0].Insert(10, new User() { Username = string.Format("New {0}", insertId) });
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
