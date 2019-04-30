using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Prism.Navigation.Xaml;
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
        private Event _event;
        public EventSeeImagesViewModel(Event loadEvent)
        {
            _event = loadEvent;
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

        private List<string> UrlList;

        public string url;
        public async void ReloadData()
        {
            list = new ObservableCollection<TestImage>();
            com = new ServerCommunicator();

            var ids = await com.GetImages(_event);
            UrlList = new List<string>();

            foreach (var id in ids)
            {
                var item = new TestImage()
                {
                    ImageUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture/" + $"{_event.Pin}/{id}",
                    FileName = string.Format($"Id: {id}")
                };
                UrlList.Add(item.ImageUrl);
                list.Add(item);
            }

           
            Items = list;
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
                Debug.WriteLine($"Tapped {item.ImageUrl}");
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
            downloader.Downloading += Downloader_DownloadReady;
            downloader.DownloadStarted += delegate(int count) { DownloadProgress = $"Downloading {0}/{count}";
                Count = count;
            };
            downloader.DownloadAllImages(UrlList);
        }

        private int Progress = 0;
        private int Count = 0;
        private void Downloader_DownloadReady(ImageDownloadEventArgs e)
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
