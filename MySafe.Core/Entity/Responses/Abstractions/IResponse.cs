using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Core.Entities.Responses.Abstractions
{
    public interface IResponse
    {
        string Error { get; set; }
        bool HasError { get; }
        byte[] FileBytes { get; set; }
    }
}
