namespace MySafe.Core
{
    public partial class MySafeApp
    {
        public class Resources
        {
            public static string PasswordPath => "ApplicationPassword";
            public static string ServerHost = "https://mysafeonline.com/";
            public static int DefaultApplicationPasswordLength = 5;
            public static string ItemId => nameof(ItemId);
            public static string ItemName => nameof(ItemName);
        }
    }
}
