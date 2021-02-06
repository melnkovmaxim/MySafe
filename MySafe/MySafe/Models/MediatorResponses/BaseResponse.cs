using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe.Models.MediatorResponses
{
    public class BaseResponse
    {
        public string Error { get; set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
    }
}
