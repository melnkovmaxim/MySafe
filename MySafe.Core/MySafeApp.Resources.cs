using System;

namespace MySafe.Core
{
    public partial class MySafeApp
    {
        public class Resources
        {
            public static string PasswordPath => "ApplicationPassword";
            public static string ServerHost = "https://mysafeonline.com/";
            public static int RequiredLengthDevicePwd = 5;
            public static TimeSpan DefaultTaskDelay => TimeSpan.FromSeconds(0.4);
            public static TimeSpan DefaultVibrationDuration => TimeSpan.FromSeconds(0.2);
        }
    }
}
