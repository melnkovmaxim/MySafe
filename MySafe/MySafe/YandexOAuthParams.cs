using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySafe
{
    public class YandexOAuthParams
    {
        public static string ClientId => "78b03bd8b07e42e5b87eba2784abf7ad";
        public static string Scope => "cloud_api:disk.app_folder";
        public static Uri AuthUrl => new Uri("https://oauth.yandex.ru/authorize");
        public static Uri RedirectUrl => new Uri("https://yx78b03bd8b07e42e5b87eba2784abf7ad.oauth.yandex.ru/auth/finish?platform=android");
    }
}
