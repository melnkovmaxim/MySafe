using MySafe.Core;
using MySafe.Presentation.Views;
using Prism.Navigation;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using MySafe.Business.Extensions;
using MySafe.Core.Commands;
using MySafe.Data.Abstractions;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedViewModelBase : ViewModelBase, INavigatedAware
    {
        protected INavigationParameters _parameters;
        protected int? _itemId;
        protected string _itemName;
        public AsyncCommand LoadedCommand { get; }

        protected AuthorizedViewModelBase(INavigationService navigationService) 
            : base(navigationService)
        {
            LoadedCommand ??= new AsyncCommand(ActionAfterLoadPage);
        }

        protected abstract Task ActionAfterLoadPage();

        protected NavigationParameters GetItemNaviigationParams(int itemId, string itemName)
        {
            return new NavigationParameters()
            {
                { MySafeApp.Resources.ItemId, itemId },
                { MySafeApp.Resources.ItemName, itemName }
            };
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add(nameof(MySafeApp.Resources.ItemId), _itemId);
            parameters.Add(nameof(MySafeApp.Resources.ItemName), _itemName);
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _parameters = parameters;

            _itemId = (int?) parameters[nameof(MySafeApp.Resources.ItemId)];
            _itemName = (string) parameters[nameof(MySafeApp.Resources.ItemName)];

            var jwtToken = await Ioc.Resolve<ISecureStorageRepository>()
                .GetJwtSecurityTokenAsync();

            if (!jwtToken.IsValidToken())
            {
                await _navigationService.NavigateAsync(nameof(SignInPage));
                return;
            }

            await LoadedCommand.ExecuteAsync(null);
        }
    }
}
