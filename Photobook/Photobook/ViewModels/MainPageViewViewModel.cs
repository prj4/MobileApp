using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class MainPageViewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Host> _hosts;

        private ObservableCollection<string> _tekst;

        public MainPageViewViewModel()
        {
            AddCommand = new Command(param =>
            {
                tekst.Add("Ny tekst streng!!!");
                //Users.Add(new User() { ImageUrl = "ccc", Date = DateTime.Now, Key = 12, Username = "Hans peter" });
                //NotifyPropertyChanged();
            });
        }

        public ObservableCollection<Host> Hosts
        {
            get => _hosts;
            set
            {
                _hosts = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> tekst
        {
            get => _tekst;
            set
            {
                _tekst = value;
                NotifyPropertyChanged();
            }
        }


        public ICommand AddCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}