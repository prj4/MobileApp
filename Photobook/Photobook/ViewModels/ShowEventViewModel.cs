using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Prism.Commands;
using System.Windows.Input;
using Photobook.Models;
using System.Linq;
using Photobook.View;
using Xamarin.Forms.Internals;
using Event = Photobook.Models.Event;

namespace Photobook.ViewModels
{
    public class EventViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation Navigation;
        private Event _event;
        private bool _showTopBar;
        private bool _showLogoutBtn;
        private IMediaUploader MedUploader;
        private Guest _guest;

        public EventViewModel(Event newEvent, bool ShowNavBar)
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

        public EventViewModel(Event newEvent, Guest currentGuest, bool ShowNavBar)
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

        private void NotifyDoneHandler(MediaEventArgs e)
        {
            Page p = new Page();
            p.IsVisible = true;
            string status = e.SendSucceeded ? "succeeded" : "failed";
            p.DisplayAlert("Upload", $"Upload {status}!", "Ok");
        }

        public bool ShowTopBar
        {
            get { return _showTopBar; }
            set { _showTopBar = value; NotifyPropertyChanged(); }
        }

        public bool ShowLogoutBtn
        {
            get { return _showLogoutBtn; }
            set { _showLogoutBtn = value; NotifyPropertyChanged(); }
        }

        public string EventName
        {
            get { return _event.Name; }
            set { _event.Name = value; NotifyPropertyChanged(); }
        }



        public string Date 
        {
            get { return $"Dato: {_event.StartDate} - {_event.EndDate}"; }
            private set { }
        }



        public string PIN
        {
            get { return $"PIN: {_event.Pin}"; }
        }

        public string Description
        {
            get { return _event.Description; }
        }

        public string Location
        {
            get { return _event.Location; }
        }

        #region Commands


        private ICommand _deleteEventCommand;
        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand ?? (_deleteEventCommand = new DelegateCommand(DeleteEvent_Execute)); }
        }

        private void DeleteEvent_Execute()
        {

        }

        private ICommand _logOutCommand;
        public ICommand LogoutCommand
        {
            get { return _logOutCommand ?? (_logOutCommand = new DelegateCommand(Logout_Execute)); }
        }

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
        public ICommand SeeImagesCommand
        {
            get { return _seeImagesCommand ?? (_seeImagesCommand = new DelegateCommand(SeeImages_Execute)); }
        }

        private async void SeeImages_Execute()
        {
            await Navigation.PushAsync(new Test(_event));
        }

        private ICommand _uploadPictureCommand;
        public ICommand UploadPictureCommand
        {
            get { return _uploadPictureCommand ?? (_uploadPictureCommand = new DelegateCommand(UploadPicture_Execute)); }
        }

        private async void UploadPicture_Execute()
        {
            IMediaPicker Med = new CrossMediaPicker();

            string photoPath = await Med.SelectPhoto();

            if (photoPath != "Null")
            {
                MedUploader.SendMedia(photoPath, _event.Pin, DataType.Picture);
            }
        }
        private ICommand _uploadVideoCommand;
        public ICommand UploadVideoCommand
        {
            get { return _uploadVideoCommand ?? (_uploadVideoCommand = new DelegateCommand(UploadVideo_Execute)); }
        }

        private async void UploadVideo_Execute()
        {
            IMediaPicker Med = new CrossMediaPicker();

            var videoPath = await Med.SelectVideo();
            
            if (videoPath != "Null")
            {

            }
        }

        private ICommand _takePhotoCommand;
        public ICommand TakePhotoCommand
        {
            get { return _takePhotoCommand ?? (_takePhotoCommand = new DelegateCommand(TakePhoto_Execute)); }
        }

        private async void TakePhoto_Execute()
        {
            ICameraAPI Cam = new CrossCamera();
            string path = await Cam.TakePhoto();
            if (path != "Null")
            {
                MedUploader.SendMedia(path, _event.Pin, DataType.Picture);
            }
        }

        private ICommand _takeVideoCommand;
        public ICommand TakeVideoCommand
        {
            get { return _takeVideoCommand ?? (_takeVideoCommand = new DelegateCommand(TakePhoto_Execute)); }
        }

        private async void TakeVideo_Execute()
        {
            ICameraAPI Cam = new CrossCamera();
            string path = await Cam.TakeVideo();
            if (path != "Null")
            {

            }
        }


        #endregion


    }
}
