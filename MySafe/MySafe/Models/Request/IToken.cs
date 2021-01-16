using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models.Request
{
    public interface IToken
    {
        string AccessToken { get; set; }
    }
}
