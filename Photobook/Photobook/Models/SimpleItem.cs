using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Photobook.Models
{
    public class SimpleItem : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        string title;
        public string Title
        {
            get { return title; }
            set 
            {
                title = value;
                NotifyPropertyChanged(); 
            }
        }

        public Color Color { get; private set; } = Color.Aqua;

    }
}
