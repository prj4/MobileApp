﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private ICommand _downloadAllCommand;
        private readonly Event _event;


        private ObservableCollection<TestImage> _items;


        private ICommand _itemTappedCommand;


        private object _lastTappedItem;
        private IMediaDownloader Downloader;
        public INavigation Navigation;

        private readonly List<string> UrlList = new List<string>();

        public TestViewModel(Event loadEvent)
        {
            _event = loadEvent;
            ReloadData();
        }

        public ObservableCollection<TestImage> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        public object LastTappedItem
        {
            get => _lastTappedItem;
            set
            {
                _lastTappedItem = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ItemTappedCommand =>
            _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand(itemTapped_Execute));

        public ICommand DownloadAllCommand =>
            _downloadAllCommand ?? (_downloadAllCommand = new DelegateCommand(DownloadAll_Execute));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Downloader_DownloadReady(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                var rootPath = DependencyService.Get<IFileDirectoryAPI>().GetImagePath();
                var fileName = $"photobook{DateTime.Now.ToString("yyyyMMddHHmmss")}.PNG";
                var fullPath = $"{rootPath}/{fileName}";

                File.WriteAllBytes(fullPath, e.FileBytes);
            }
        }

        public async void ReloadData()
        {
            var list = new ObservableCollection<TestImage>();
            var com = new ServerCommunicator();

            var urlList = await com.GetImages(_event);


            foreach (var id in urlList)
            {
                var item = new TestImage
                {
                    ImageUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture/" + $"{_event.Pin}/{id}",
                    FileName = string.Format($"Id: {id}")
                };
                UrlList.Add(item.ImageUrl);
                list.Add(item);
                //completeUrl.Add("https://photobookwebapi1.azurewebsites.net/api/Picture/" + $"{e.Pin}/{id}");
            }

            // serverCommunicator.GetImages(_event);
            // Her skal alle Id'er hentes
            // Så tænker jeg, at for alle ID'er der er, så skal de bare hentes således:
            // photobookwebapi1.azurewebsites.net{eventpin]/id
            // Så et for loop, der henter alle, indsætter ID'et i URL'en
            // Og indsætter det rigtige ID til sidst
            // Dvs. at for at vise billedet skal jeg bare have et link som dette: 
            // https://photobookwebapi1.azurewebsites.net/api/Picture/rine2164bk/4

            // Hvert link indsættes "Items"
            // Eventuel "FileName" bør være navnet på brugeren der har taget billedet

            // Alt det her under er bare dummy data


            //string[] images = {
            //    "https://farm2.staticflickr.com/1227/1116750115_b66dc3830e.jpg",
            //    "https://farm8.staticflickr.com/7351/16355627795_204bf423e9.jpg",
            //    "https://farm1.staticflickr.com/44/117598011_250aa8ffb1.jpg",
            //    "https://farm8.staticflickr.com/7524/15620725287_3357e9db03.jpg",
            //    "https://farm9.staticflickr.com/8351/8299022203_de0cb894b0.jpg",
            //    "https://images.unsplash.com/photo-1555863040-89ea8aa29a36?ixlib=rb-1.2.1&auto=format&fit=crop&w=1191&q=80",
            //    "https://photobookwebapi1.azurewebsites.net/api/Picture/rine2164bk/4"
            //};

            //for (int n = 0; n < images.Length; n++)
            //{
            //    var item = new TestImage()
            //    {
            //        ImageUrl = images[n],
            //        FileName = string.Format("Oskar, gudesønnen")
            //    };

            //    list.Add(item);
            //}

            Items = list;
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

        private async void DownloadAll_Execute()
        {
            var cookies = SettingsManager.CurrentCookies;
            Downloader = new MediaDownloader(cookies);
            Downloader.DownloadReady += Downloader_DownloadReady;

            if (UrlList != null && UrlList.Count > 0)
                Downloader.DownloadAllImages(UrlList);
        }
    }
}