using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using PCLStorage;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Prism.Navigation.Xaml;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<string> UrlList = new List<string>();
        private IMediaDownloader Downloader;
        public INavigation Navigation;
        private Event _event;
        public TestViewModel(Event loadEvent)
        {
            _event = loadEvent;
            ReloadData();
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
            
            UrlList  = await com.GetImages(_event);
            


            foreach (var id in UrlList)
            {
                var item = new TestImage()
                {
                    ImageUrl = "https://photobookwebapi1.azurewebsites.net/api/Picture/" + $"{_event.Pin}/{id}",
                    FileName = string.Format($"Id: {id}")
                };
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
                System.Diagnostics.Debug.WriteLine($"Tapped {item.ImageUrl}");
                    Navigation.PushAsync(new EventSeeSingleImage(item));
            }

        }


        private ICommand _downloadAllCommand;
        public ICommand DownloadAllCommand
        {
            get { return _downloadAllCommand ?? (_downloadAllCommand = new DelegateCommand(DownloadAll_Execute)); }
        }

        private async void DownloadAll_Execute()
        {
            Downloader = new MediaDownloader(await SettingsManager.GetCookies(_event.Pin));
            Downloader.DownloadReady += Downloader_DownloadReady;

            if(UrlList != null)
                Downloader.DownloadAllImages(UrlList);
        }

    }
}
