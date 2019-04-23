using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Photobook.Models;
using Xamarin.Forms;

namespace Photobook.ViewModels
{
    public class EventSeeSingleImageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        


        public INavigation Navigation;
        public EventSeeSingleImageViewModel(TestImage Img)
        {
            Image = Img;
        }

        private TestImage _image;
        public TestImage Image
        {
            get { return _image; }
            set { _image = value; NotifyPropertyChanged(); }
        }

        private string _pictureTaker;

        public string PictureTaker
        {
            get { return $"Taken by: {Image.FileName}"; }
            set { _pictureTaker = value; NotifyPropertyChanged(); }
        }

    }
}
