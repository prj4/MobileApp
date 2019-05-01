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
using PB.Dto;
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
        private EventModel _event;
        private ObservableCollection<TestImage> list;
        private ServerCommunicator com;

        public EventSeeImagesViewModel(EventModel loadEvent)
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
            com = new ServerCommunicator();
            list = new ObservableCollection<TestImage>();

            var ids = await com.GetImages(_event);
            UrlList = new List<string>();

            var images = new List<string>();
            foreach (var id in ids)
            {
                images.Add("https://photobookwebapi1.azurewebsites.net/api/Picture/Preview/" + $"{_event.Pin}/{id}");

                UrlList.Add("https://photobookwebapi1.azurewebsites.net/api/Picture/" + $"{_event.Pin}/{id}");
            }

            IMediaDownloader downloader = new MediaDownloader(SettingsManager.CurrentCookies);
            downloader.Downloading += Downloader_DownloadPreview;
            downloader.DownloadAllImages(images);
            
            
        }

        private void Downloader_DownloadPreview(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                var path = DependencyService.Get<IFileDirectoryAPI>().GetTempPath() + '/';
                var fileName = $"_temp{DateTime.Now.ToString("yyMMddHHmmss")}.PNG";
                var fullPath = path + fileName;

                File.WriteAllBytes(fullPath, e.FileBytes);

                list.Add(new TestImage
                {
                    FileName = fileName.Substring(5, 12),
                    ImagePath = fullPath,
                    ImageUriPath = new Uri(fullPath)
                });
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
