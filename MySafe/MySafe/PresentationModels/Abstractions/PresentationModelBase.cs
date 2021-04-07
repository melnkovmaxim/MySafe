﻿namespace MySafe.Presentation.Models.Abstractions
{
    public class PresentationModelBase : IPresentationModel
    {
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        public byte[] FileBytes { get; set; }
    }
}