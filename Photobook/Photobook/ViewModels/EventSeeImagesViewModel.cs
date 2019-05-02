
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

        public INavigation Navigation;
        private EventModel _event;
        private List<string> Images;
        private ServerCommunicator com;

        public EventSeeImagesViewModel(EventModel loadEvent)
        {
            Items = new ObservableCollection<TestImage>();
            Images = new List<string>();
            UrlList = new List<string>();

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

        private List<string> UrlList = new List<string>();

        public string url;
        public async void ReloadData()
        {
            com = new ServerCommunicator();

            var ids = await com.GetImages(_event);

            if (ids.Count != Images.Count)
            {
                foreach (var id in ids)
                {
                    var preview = UrlFactory.Generate(DataType.GetPreview) + $"/{_event.Pin}/{id}";
                    var full = UrlFactory.Generate(DataType.GetPicture) + $"/{_event.Pin}/{id}";

                    if (!Images.Contains(preview))
                        Images.Add(preview);
                    if (UrlList.Contains(full))
                        UrlList.Add(full);
                }
            }
            
            
            IMediaDownloader downloader = new MediaDownloader(SettingsManager.CurrentCookies);
            downloader.Downloading += Downloader_DownloadPreview;
            downloader.DownloadAllImages(Images);
            
        }
        

        private void Downloader_DownloadPreview(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                var path = DependencyService.Get<IFileDirectoryAPI>().GetTempPath();
                var fileName = $"_temp_{e.PictureId}{Directory.GetFiles(path).Length}.PNG";
                var fullPath = $"{path}/{fileName}";

                File.WriteAllBytes(fullPath, e.FileBytes);

                Items.Add(new TestImage
                {
                    FileName = e?.PictureId,
                    ImagePath = fullPath,
                    Source = ImageSource.FromFile(fullPath),
                    PinId = e.PinId
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
                var fileName = $"/photobook{Directory.GetFiles(directoryPath).Length+ 1}.PNG";
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

        private void DeleteTempDirectory()
        {
            Array.ForEach(
                Directory.GetFiles(DependencyService.Get<IFileDirectoryAPI>().GetTempPath()),
                    delegate(string path)
                    {
                        File.Delete(path);
                    });
        }
        public void OnLeave(object sender, EventArgs e)
        {
            if(sender != this)
                new Thread(DeleteTempDirectory).Start();

            Debug.WriteLine("Farvel");
        }
    }
}
