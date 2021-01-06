using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace MySafe.Services
{
    public class YandexAuthService
    {
        private readonly OAuth2Authenticator _authenticator;
        private bool _isAuthenticated;
        static EventWaitHandle handle = new AutoResetEvent(false);
        public YandexAuthService()
        {

            _authenticator = new OAuth2Authenticator(YandexOAuthParams.ClientId, YandexOAuthParams.Scope, YandexOAuthParams.AuthUrl, YandexOAuthParams.RedirectUrl, null, false);

            _authenticator.Completed += Complete;
        }

        public async Task<bool> Login()
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();

            presenter.Login(_authenticator);

            return _isAuthenticated;
        }
        
        private async void Complete(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                await SecureStorage.SetAsync("account", e.Account.Serialize());
            }

            _isAuthenticated = e.IsAuthenticated;
        }
    }
}
