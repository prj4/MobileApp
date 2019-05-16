﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
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

        #region Attributes

        public INavigation Navigation;
        private readonly EventModel _event;
        private List<string> Images;
        private ServerCommunicator com;
        private string downloadProgress = "";
        private int Progress = 0;
        private int Count = 0;
        private object _lock = new object();
        private Stopwatch sw = new Stopwatch();

        #endregion

        #region DataBindings
        
        private static ObservableCollection<TestImage> _items;
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

        public string DownloadProgress
        {
            get { return downloadProgress; }
            set
            {
                downloadProgress = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Constructors

        public EventSeeImagesViewModel(EventModel loadEvent)
        {
            Items = new ObservableCollection<TestImage>();
            Images = new List<string>();

            _event = loadEvent;
            
            ReloadData();
        }

        #endregion

        #region Methods


        public async void ReloadData()
        {
            DeleteTempDirectory();
            Refresh = true;
            com = new ServerCommunicator();

            var ids = await com.GetImages(_event, MemoryManager.CurrentCookies);

            foreach (var id in ids)
            {
                Images.Add(UrlFactory.Generate(DataType.GetPreview) + $"/{_event.Pin}/{id}");
            }


            IMediaDownloader downloader = new MediaDownloader(MemoryManager.CurrentCookies);
            downloader.Downloading += Downloader_DownloadPreview;
            sw.Start();
            downloader.DownloadAllImages(Images);

        }

        public bool Refresh = false;

        private void Downloader_DownloadPreview(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                Refresh = false;
                string fullPath;
                lock (_lock)
                {

                    fullPath = MemoryManager.SaveToTemp(e.FileBytes, e.PictureId);
                }
                sw.Stop();
                Debug.WriteLine($"{sw.ElapsedMilliseconds} ms", "Downloaded image");
                sw.Reset();
                sw.Start();
                var list = Items;

                list.Add(new TestImage
                {
                    FileName = e?.PictureId,
                    ImagePath = fullPath,
                    Source = ImageSource.FromFile(fullPath),
                    PinId = e.PinId
                });

                Items = list;
                NotifyPropertyChanged("Items");
            }
            else
            {
            }
        }

        private void DeleteTempDirectory()
        {
            MemoryManager.PurgeTempDirectory();
        }
        private void Downloader_DownloadFull(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                MemoryManager.SaveToPicture(e.FileBytes, e.PictureId);
                DownloadProgress = $"Downloading {Progress++}/{Count}";
                if (Progress == Count)
                {
                    Progress = 0;
                    DownloadProgress = "Done!";
                }

            }
        }
        #endregion

        #region Commands

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand
        {
            get { return _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand(itemTapped_Execute)); }
        }

        private void itemTapped_Execute()
        {
            if (LastTappedItem is TestImage item)
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
            var UrlList = new List<string>();

            foreach (var item in Items)
            {
                UrlList.Add(item.FullPictureUrl);
            }

            var cookies = MemoryManager.CurrentCookies;
            IMediaDownloader downloader = new MediaDownloader(cookies);
            downloader.Downloading += Downloader_DownloadFull;
            downloader.DownloadStarted += delegate (int count) {
                DownloadProgress = $"Downloading {0}/{count}";
                Count = count;
            };
            downloader.DownloadAllImages(UrlList);
        }

        #endregion
        
       
    }
}
