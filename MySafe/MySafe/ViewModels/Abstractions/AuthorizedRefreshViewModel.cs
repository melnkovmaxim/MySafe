using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySafe.Core.Commands;
using MySafe.Core.Entities.Responses.Abstractions;

namespace MySafe.Presentation.ViewModels.Abstractions
{
    public abstract class AuthorizedRefreshViewModel<TResult> : AuthorizedViewModelBase
        where TResult : IResponse
    {
        public AsyncCommand RefreshCommand { get; }

        public AuthorizedRefreshViewModel()
        {

            RefreshCommand = new AsyncCommand(async () =>
            {
                var result = await GetRefreshTask();
                var hasError = HandleRefreshResult(result);
                if (!hasError) RefillObservableCollection(result);
            });
        }

        protected abstract Task<TResult> GetRefreshTask();
        protected abstract void RefillObservableCollection(TResult mediatorResponse);

        protected virtual bool HandleRefreshResult(IResponse mediatorResponse)
        {
            Error = mediatorResponse.Error;

            return mediatorResponse.HasError;
        }
    }
}
