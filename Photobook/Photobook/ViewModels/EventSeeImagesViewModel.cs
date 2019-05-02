using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class EventSeeImagesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;
        private EventModel _event;
        private ServerCommunicator com;

        public EventSeeImagesViewModel(EventModel loadEvent)
        {
            _event = loadEvent;
            Items = new ObservableCollection<TestImage>();
            ReloadData();
        }

        //private bool showProgress = false;

        //public bool ShowProgress
        //{
        //    get { return showProgress; }
        //    set { showProgress = value; }
        //}

        private string downloadProgress = "";

        public string DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                downloadProgress = value;
                NotifyPropertyChanged();
            }
        }

        private static List<string> UrlList = new List<string>();

        public string url;
        public async void ReloadData()
        {
            com = new ServerCommunicator();

            var ids = await com.GetImages(_event);

            if (ids.Count > UrlList.Count)
            {
                var images = new List<string>();
                for(int i = (UrlList.Count == 0) ? 0 : UrlList.Count - 1; i < ids.Count; i++)
                {

                    images.Add(UrlFactory.Generate(DataType.GetPreview) + $"{_event.Pin}/{ids[i]}");

                    UrlList.Add(UrlFactory.Generate(DataType.GetPicture) + $"{_event.Pin}/{ids[i]}");
                }

                IMediaDownloader downloader = new MediaDownloader(SettingsManager.CurrentCookies);
                downloader.Downloading += Downloader_DownloadPreview;
                downloader.DownloadAllImages(images);
            }
            
            
        }

        private void Downloader_DownloadPreview(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                var path = DependencyService.Get<IFileDirectoryAPI>().GetTempPath() + '/';
                var fileName = $"_temp{DateTime.Now.ToString("yyMMddHHmmss")}.PNG";
                var fullPath = path + fileName;

                File.WriteAllBytes(fullPath, e.FileBytes);

                Items.Add(new TestImage
                {
                    FileName = fileName.Substring(5, 12),
                    ImagePath = fullPath,
                    Source = ImageSource.FromFile(fullPath)
                });
                NotifyPropertyChanged();
                Debug.WriteLine("ImageStatus okay", "ImageStatus");
            }
            else
            {
                Debug.WriteLine("ImageStatus not okay", "ImageStatus");
            }
        }

        private ObservableCollection<TestImage> _items;
        public ObservableCollection<TestImage> Items
        {
            get { return _items; }
            set { _items = value; NotifyPropertyChanged(); }
        }


        private object _lastTappedItem;
        public object LastTappedItem
        {
            get { return _lastTappedItem; }
            set { _lastTappedItem = value; NotifyPropertyChanged(); }
        }



        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand
        {
            get { return _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand(itemTapped_Execute)); }
        }

        private void itemTapped_Execute()
        {
            var item = LastTappedItem as TestImage;
            if (item != null)
            {
                Debug.WriteLine($"Tapped {item.ImagePath}");
                    Navigation.PushAsync(new EventSeeSingleImage(item));
            }

        }


        private ICommand _downloadAllCommand;
        public ICommand DownloadAllCommand
        {
            get { return _downloadAllCommand ?? (_downloadAllCommand = new DelegateCommand(DownloadAll_Execute)); }
        }

        private void DownloadAll_Execute()
        {
            var cookies = SettingsManager.CurrentCookies;
            IMediaDownloader downloader = new MediaDownloader(cookies);
            downloader.Downloading += Downloader_DownloadFull;
            downloader.DownloadStarted += delegate(int count) { DownloadProgress = $"Downloading {0}/{count}";
                Count = count;
            };
            downloader.DownloadAllImages(UrlList);
        }

        private int Progress = 0;
        private int Count = 0;
        private void Downloader_DownloadFull(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                var directoryPath = DependencyService.Get<IFileDirectoryAPI>().GetImagePath();
                var fileName = $"/photobook{DateTime.Now.ToString("yyMMddHHmmss")}.PNG";
                var fullPath = directoryPath + fileName;
                File.WriteAllBytes(fullPath, e.FileBytes);
                DownloadProgress = $"Downloading {Progress++}/{Count}";
                if (Progress == Count)
                {
                    Progress = 0;
                    DownloadProgress = "Done!";
                }
                
            }
        }
    }
}
