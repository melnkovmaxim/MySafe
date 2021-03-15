﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FolderPage : ContentPage
    {
        public FolderPage()
        {
            InitializeComponent();
            
            NavigationPage.SetBackButtonTitle(this, "HELLO");
        }
    }
}