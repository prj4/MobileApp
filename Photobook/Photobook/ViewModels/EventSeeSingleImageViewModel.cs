using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

        private ICommand downloadSingleCommand;
        public INavigation Navigation;
        private IMemoryManager _memoryManager;

        public EventSeeSingleImageViewModel(TestImage Img, IMemoryManager memoryManager = null)
        {
            Image = Img;
            _memoryManager = memoryManager ?? MemoryManager.GetInstance();
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

        private ICommand _deleteImageCommand;
        public ICommand DeleteImageCommand => _deleteImageCommand ?? (_deleteImageCommand = new DelegateCommand(DeleteImage_Execute));

        private async void DeleteImage_Execute()
        {
            IServerCommunicator com = new ServerCommunicator();
            com.AddCookies(_memoryManager.CurrentCookies);

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
            var cookies = _memoryManager.CurrentCookies;
            if (cookies != null)
            {
                IMediaDownloader downloader = new MediaDownloader(cookies);
                downloader.DownloadSingleImage(Image.FullPictureUrl);
                downloader.Downloading += args => { _memoryManager.SaveToPicture(args.FileBytes, args.PictureId); };
            }
        }
    }
}