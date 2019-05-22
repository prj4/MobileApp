
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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
        private IMemoryManager _memoryManager;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Attributes

        public INavigation Navigation;
        private readonly EventModel _event;
        private List<string> Images;
        private IServerCommunicator com;
        private string downloadProgress = "";
        private int Progress = 0;
        private int Count = 0;
        private object _lock = new object();
        private Stopwatch sw = new Stopwatch();

        #endregion

        #region DataBindings

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; NotifyPropertyChanged(); }
        }
        
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
            com = new ServerCommunicator();
            Items = new ObservableCollection<TestImage>();
            Images = new List<string>();

            _memoryManager = MemoryManager.GetInstance();

            _event = loadEvent;
            
            ReloadData();
        }

        #endregion

        #region Methods


        public async void ReloadData()
        {
#if !DEBUG
            DeleteTempDirectory();
#endif
            
            
            Refresh = true;

            var ids = await com.GetImages(_event, _memoryManager.CurrentCookies);

            foreach (var id in ids)
            {
                Images.Add(UrlFactory.Generate(DataType.GetPreview) + $"/{_event.Pin}/{id}");
            }


            IMediaDownloader downloader = new MediaDownloader(_memoryManager.CurrentCookies);
            downloader.Downloading += Downloader_DownloadPreview;
            sw.Start();
            downloader.DownloadAllImages(Images);

        }

        public bool Refresh = false;

        private ObservableCollection<TestImage> list;


        private void Downloader_DownloadPreview(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                Refresh = false;
                string fullPath;
                lock (_lock)
                {

                    fullPath = _memoryManager.SaveToTemp(e.FileBytes, e.PictureId);
                }
                sw.Stop();
                Debug.WriteLine($"{sw.ElapsedMilliseconds} ms", "Downloaded image");
                sw.Reset();
                sw.Start();
                list = Items;

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
            _memoryManager.PurgeTempDirectory();
        }
        private void Downloader_DownloadFull(ImageDownloadEventArgs e)
        {
            if (e.StatusOk)
            {
                _memoryManager.SaveToPicture(e.FileBytes, e.PictureId);
                DownloadProgress = $"Downloading {Progress++}/{Count}";
                if (Progress == Count)
                {
                    Progress = 0;
                    DownloadProgress = "Done!";
                    // Indsat her
                    Items = list;
                }

            }
        }
        #endregion

   

        #region Commands


        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new DelegateCommand(refresh_execute)); }
        }
        private void refresh_execute()
        {
            IsRefreshing = true;
            Items = list;
            IsRefreshing = false;

        }

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand
        {
            get { return _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand(itemTapped_Execute)); }
        }

        private async void itemTapped_Execute()
        {
            if (LastTappedItem is TestImage item)
            {

                await Navigation.PushAsync(new EventSeeSingleImage(item));
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

            var cookies = _memoryManager.CurrentCookies;
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
