using System.Collections.Generic;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Core.Models.Responses.Abstractions
{
    public class ResponseList<T> : List<T>, IResponse
    {
        public string Error { get; set; }
        public bool HasError { get; }
        public byte[] FileBytes { get; set; }
    }
}