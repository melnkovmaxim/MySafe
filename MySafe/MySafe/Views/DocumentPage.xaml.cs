﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySafe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentPage : ContentPage
    {
        public DocumentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}