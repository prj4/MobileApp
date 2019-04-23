using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Photobook.View;
using Prism.Commands;
using Prism.Navigation.Xaml;
using Xamarin.Forms;

namespace Photobook.Models
{
    public class TestImage
    {
        public string ImageUrl { get; set; }

        public string FileName { get; set; }
    }
}
