using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core;
using RestSharp;

namespace MySafe.Services
{
    public class RestClientWrapper: RestClient
    {
        public RestClientWrapper()
            :base(MySafeApp.Resources.ServerHost)
        { 
            Timeout = 10 * 1000; // 10 sec
        }
    }
}
