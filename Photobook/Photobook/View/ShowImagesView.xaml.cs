﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Photobook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        
        public Test(Event loadEvent)
        {
            var vm = new TestViewModel(loadEvent);
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();
        }
    }
}