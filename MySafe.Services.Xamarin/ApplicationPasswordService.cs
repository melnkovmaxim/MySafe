using MySafe.Core;
using MySafe.Core.Entities;
using MySafe.Services.Extensions;

namespace MySafe.Services.Xamarin
{
    public class ApplicationPasswordService
    {
        public ApplicationPassword GetPasswordWithDefaultLength()
        {
            var password = new ApplicationPassword();

            password.SetPasswordLength(MySafeApp.Resources.DefaultApplicationPasswordLength);

            return password;
        }
    }
}