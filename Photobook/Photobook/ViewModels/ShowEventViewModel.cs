using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PB.Dto;
using Photobook.Models;
using Photobook.View;
using Prism.Commands;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class ShowEventViewModel : INotifyPropertyChanged
    {
        private readonly EventModel _event;
        private readonly Guest _guest;
        private bool _showLogoutBtn;
        private bool _showTopBar;
        private readonly IMediaUploader MedUploader;

        public INavigation Navigation;

        public ShowEventViewModel(EventModel newEvent, bool ShowNavBar)
        {
            _event = newEvent;
            ShowTopBar = ShowNavBar;
            _guest = new Guest();
            MedUploader = new MediaUploader(_guest);
            MedUploader.NotifyDone += NotifyDoneHandler;
            if (ShowNavBar)
                ShowLogoutBtn = false;
            else
                ShowLogoutBtn = true;
        }

        public ShowEventViewModel(EventModel newEvent, Guest currentGuest, bool ShowNavBar)
        {
            _event = newEvent;
            _guest = currentGuest;
            ShowTopBar = ShowNavBar;
            MedUploader = new MediaUploader(_guest);
            MedUploader.NotifyDone += NotifyDoneHandler;
            if (ShowNavBar)
                ShowLogoutBtn = false;
            else
                ShowLogoutBtn = true;
        }

        public bool ShowTopBar
        {
            get => _showTopBar;
            set
            {
                _showTopBar = value;
                NotifyPropertyChanged();
            }
        }

        public bool ShowLogoutBtn
        {
            get => _showLogoutBtn;
            set
            {
                _showLogoutBtn = value;
                NotifyPropertyChanged();
            }
        }

        public string EventName
        {
            get => _event.Name;
            set
            {
                _event.Name = value;
                NotifyPropertyChanged();
            }
        }


        public string Date
        {
            get => $"Dato: {_event.StartDate} - {_event.EndDate}";
            private set { }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyDoneHandler(MediaEventArgs e)
        {
            var p = new Page();
            p.IsVisible = true;
            var status = e.SendSucceeded ? "succeeded" : "failed";
            p.DisplayAlert("Upload", $"Upload {status}!", "Ok");
        }

        #region Commands

        private ICommand _deleteEventCommand;

        public ICommand DeleteEventCommand =>
            _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand(DeleteEvent_Execute));

        private void DeleteEvent_Execute()
        {
        }

        private ICommand _logOutCommand;

        public ICommand LogoutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(Logout_Execute));

        private void Logout_Execute()
        {
            var rootPage = Navigation.NavigationStack.FirstOrDefault();
            if (rootPage != null)
            {
                Navigation.InsertPageBefore(new StartUpView(), Navigation.NavigationStack.First());
                Navigation.PopToRootAsync();
            }
        }

        private ICommand _seeImagesCommand;

        public ICommand SeeImagesCommand =>
            _seeImagesCommand ?? (_seeImagesCommand = new DelegateCommand(SeeImages_Execute));

        private void SeeImages_Execute()
        {
            Navigation.PushAsync(new EventSeeImages(_event), true);
        }

        private ICommand _uploadPictureCommand;

        public ICommand UploadPictureCommand => _uploadPictureCommand ??
                                                (_uploadPictureCommand = new DelegateCommand(UploadPicture_Execute));

        private async void UploadPicture_Execute()
        {
            IMediaPicker Med = new CrossMediaPicker();

            var photoPath = await Med.SelectPhoto();

            if (photoPath != "Null") MedUploader.SendMedia(photoPath, _event.Pin, DataType.Picture);
        }

        private ICommand _uploadVideoCommand;

        public ICommand UploadVideoCommand =>
            _uploadVideoCommand ?? (_uploadVideoCommand = new DelegateCommand(UploadVideo_Execute));

        private async void UploadVideo_Execute()
        {
            IMediaPicker Med = new CrossMediaPicker();

            var videoPath = await Med.SelectVideo();

            if (videoPath != "Null")
            {
            }
        }

        private ICommand _takePhotoCommand;

        public ICommand TakePhotoCommand =>
            _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(TakePhoto_Execute));

        private async void TakePhoto_Execute()
        {
            ICameraAPI Cam = new CrossCamera();
            var path = await Cam.TakePhoto();
            if (path != "Null") MedUploader.SendMedia(path, _event.Pin, DataType.Picture);
        }

        private ICommand _takeVideoCommand;

        public ICommand TakeVideoCommand =>
            _takeVideoCommand ?? (_takeVideoCommand = new DelegateCommand(TakePhoto_Execute));

        private async void TakeVideo_Execute()
        {
            ICameraAPI Cam = new CrossCamera();
            var path = await Cam.TakeVideo();
            if (path != "Null")
            {
            }
        }

        #endregion
    }
}