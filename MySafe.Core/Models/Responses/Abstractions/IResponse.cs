﻿namespace MySafe.Core.Entities.Responses.Abstractions
{
    public interface IResponse
    {
        string Error { get; set; }
        bool HasError { get; }
        byte[] FileBytes { get; set; }
    }
}