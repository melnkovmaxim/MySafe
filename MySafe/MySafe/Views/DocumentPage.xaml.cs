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
    public partial class DocumentPage : ContentPage
    {
        public DocumentPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                while (true)
                {
                    await _spinnetImage.RelRotateTo(360, 3000); 
                }
            });
        }
    }
}