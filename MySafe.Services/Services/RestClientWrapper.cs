using MySafe.Core;
using RestSharp;

namespace MySafe.Services.Services
{
    public class RestClientWrapper : RestClient
    {
        public RestClientWrapper()
            : base(MySafeApp.Resources.ServerHost)
        {
            Timeout = 120 * 1000; // 10 sec
        }
    }
}