using System.Threading.Tasks;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses.Abstractions;
using Prism.Navigation;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedRefreshViewModel<TResult> : AuthorizedViewModelBase, INavigatedAware
        where TResult : IResponse
    {
        protected AuthorizedRefreshViewModel(INavigationService navigationService) : base(navigationService)
        {
            RefreshCommand = new AsyncCommand(async () =>
            {
                var result = await _refreshTask;
                var hasError = HandleRefreshResult(result);
                if (!hasError) RefillObservableCollection(result);
            });
        }

        public AsyncCommand RefreshCommand { get; }

        protected abstract Task<TResult> _refreshTask { get; }
        protected abstract void RefillObservableCollection(TResult mediatorResponse);

        protected virtual bool HandleRefreshResult(TResult mediatorResponse)
        {
            Error = mediatorResponse.Error;

            return mediatorResponse.HasError;
        }

        protected override void DoAfterNavigatedTo()
        {
            RefreshCommand.Execute(null);
        }
    }
}