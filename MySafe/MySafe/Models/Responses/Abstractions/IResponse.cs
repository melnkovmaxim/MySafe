using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models.Responses
{
    public interface IResponse
    {
        string Error { get; set; }
        bool HasError { get; }
        byte[] FileBytes { get; set; }
    }
}
