﻿using System;
using System.Collections.Generic;
using Photobook.Models;
using Photobook.ViewModels;
using Xamarin.Forms;

namespace Photobook.View
{
    public partial class NewUser : ContentPage
    {
        public NewUser(NewUserViewModel vm)
        {
            vm.Navigation = Navigation;
            BindingContext = vm;
            InitializeComponent();

        }
    }
}
