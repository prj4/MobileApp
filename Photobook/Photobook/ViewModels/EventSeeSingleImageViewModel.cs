using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class EventSeeSingleImageViewModel : INotifyPropertyChanged
    {
        private TestImage _image;

        private string _pictureTaker;

        private ICommand downloadSingleCommand;


        public INavigation Navigation;

        public EventSeeSingleImageViewModel(TestImage Img)
        {
            Image = Img;
        }

        public TestImage Image
        {
            get => _image;
            set
            {
                _image = value;
                NotifyPropertyChanged();
            }
        }

        public string PictureTaker
        {
            get => $"Taken by: {Image.FileName}";
            set
            {
                _pictureTaker = value;
                NotifyPropertyChanged();
            }
        }
        

        private ICommand _deleteImageCommand;
        public ICommand DeleteImageCommand => _deleteImageCommand ?? (_deleteImageCommand = new DelegateCommand(DeleteImage_Execute));

        private async void DeleteImage_Execute()
        {
            IServerCommunicator com = new ServerCommunicator();

            if(await com.DeleteFromServer(_image, DataType.Picture))
            {
                Debug.WriteLine("Deleted");
            }
            else
            {
                Debug.WriteLine("Not deleted");
            }
        }

        public ICommand DownloadSingleCommand =>
            downloadSingleCommand ?? (downloadSingleCommand = new DelegateCommand(DownloadImage));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DownloadImage()
        {
            var cookies = SettingsManager.CurrentCookies;
            if (cookies != null)
            {
                IMediaDownloader downloader = new MediaDownloader(cookies);
                //downloader.DownloadSingleImage(Image.ImagePath);
            }
        }
    }
}